using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class DungeonGenerator : MonoBehaviour {

    public int numRooms = 10;

    public int roomWidthInTiles = 1;
    public int roomHeightInTiles = 1;

    public int tileWidth = 8;

    public int roomHorizontalOffset = -8;
    public int roomVerticalOffset = 0;

    public DungeonRoom roomPrefab;
    public ExitHandler exitHandlerPrefab;    

    public Transform roomParent;    

    public List<DungeonRoom> currentDungeonRooms = new List<DungeonRoom>();

    public DungeonManager dungeonManager;
    public CameraManager cameraManager;
    public GameObject player;

    public void DestroyDungeon()
    {
        foreach (DungeonRoom room in currentDungeonRooms)
        {
            Destroy(room.gameObject);
        }

        currentDungeonRooms.Clear();
    }

    [ContextMenu("Build Dungeon")]
    public List<DungeonRoom> BuildDungeon()
    {
        DestroyDungeon(); //clear any existing dungeon    

        List<SimpleDungeonRoom> generatedRooms = GenerateDungeonLayout();

        foreach (SimpleDungeonRoom room in generatedRooms)
        {
            DungeonRoom roomObject = (DungeonRoom)GameObject.Instantiate(roomPrefab);

            roomObject.transform.parent = roomParent;

            Vector2 roomPosition = room.location;
            roomPosition.x = (roomPosition.x * roomWidthInTiles * tileWidth) + roomHorizontalOffset;
            roomPosition.y = (roomPosition.y * roomHeightInTiles * tileWidth) + roomVerticalOffset;
            roomObject.transform.position = roomPosition;

            roomObject.roomCoordinates = room.location;
            roomObject.exits = new HashSet<Direction>(room.exits);

            currentDungeonRooms.Add(roomObject);

            PrepareStairs(roomObject);

            roomObject.stairsDown.gameObject.SetActive(false);
            roomObject.stairsUp.gameObject.SetActive(false);

            //roomObject.stairsDown.gameObject.SetActive(false);
            //roomObject.stairsDown.dungeonManager = dungeonManager;

            //roomObject.stairsUp.gameObject.SetActive(false);
            //roomObject.stairsUp.dungeonManager = dungeonManager;

            //build the exits
            ExitHandler exitHandler = (ExitHandler)GameObject.Instantiate(exitHandlerPrefab);

            PrepareExits(roomObject, exitHandler);

            //exitHandler.transform.parent = roomObject.transform;
            //exitHandler.transform.localPosition = Vector2.zero;

            //exitHandler.dungeonManager = dungeonManager;
            //exitHandler.cameraManager = cameraManager;
            //exitHandler.player = player;

            foreach (Direction exitDirection in room.exits)
            {
                GameObject exitBlock = null;
                ExitTrigger exitTrigger = null;

                switch (exitDirection)
                {
                    case Direction.East:
                        exitBlock = roomObject.eastExit;
                        exitTrigger = exitHandler.eastExit;
                        break;
                    case Direction.West:
                        exitBlock = roomObject.westExit;
                        exitTrigger = exitHandler.westExit;
                        break;
                    case Direction.South:
                        exitBlock = roomObject.southExit;
                        exitTrigger = exitHandler.southExit;
                        break;
                    case Direction.North:
                        exitBlock = roomObject.northExit;
                        exitTrigger = exitHandler.northExit;
                        break;
                    default:
                        break;
                }

                if (exitBlock != null)
                {
                    exitBlock.SetActive(false);
                }

                if (exitTrigger != null)
                {
                    exitTrigger.gameObject.SetActive(true);
                }
            }            
        }

        DungeonRoom firstRoom = currentDungeonRooms.First();

        firstRoom.stairsDown.gameObject.SetActive(true);


        DungeonRoom finalRoom = currentDungeonRooms.Last();

        finalRoom.stairsUp.gameObject.SetActive(true);

        return new List<DungeonRoom>(currentDungeonRooms);
    }

    public void PrepareStairs(DungeonRoom roomObject)
    {
        //roomObject.stairsDown.gameObject.SetActive(false);
        roomObject.stairsDown.dungeonManager = dungeonManager;

        //roomObject.stairsUp.gameObject.SetActive(false);
        roomObject.stairsUp.dungeonManager = dungeonManager;
    }

    public void PrepareExits(DungeonRoom roomObject, ExitHandler exitHandler)
    {
        if (exitHandler == null)
        {
            exitHandler = roomObject.gameObject.GetComponentInChildren<ExitHandler>();

            if (exitHandler == null) { return; }
        }

        exitHandler.transform.parent = roomObject.transform;
        exitHandler.transform.localPosition = Vector2.zero;

        exitHandler.dungeonManager = dungeonManager;
        exitHandler.cameraManager = cameraManager;
        exitHandler.player = player;
    }

    [ContextMenu("Display Dungeon")]
    public void DisplayDungeon()
    {
        StartCoroutine(DoDisplayDungeon());
    }

    public IEnumerator DoDisplayDungeon()
    {
        foreach (DungeonRoom room in currentDungeonRooms)
        {
            room.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);

        foreach (DungeonRoom room in currentDungeonRooms)
        {
            room.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
    }
        
    public List<SimpleDungeonRoom> GenerateDungeonLayout()
    {                
        List<SimpleDungeonRoom> dungeonRooms = new List<SimpleDungeonRoom>();

        SimpleDungeonRoom startingRoom = new SimpleDungeonRoom();
        
        startingRoom.location = Vector2.zero;

        dungeonRooms.Add(startingRoom);

        //for each remaining room, add a room on to an existing room
        for (int i = 1; i < numRooms; i++)
        {
            SimpleDungeonRoom newRoom = new SimpleDungeonRoom();            

            bool roomPlaced = false;

            //just brute force it for now
            while (!roomPlaced)
            {
                //pick a random starting room
                int randomRoomIndex = UnityEngine.Random.Range(0, dungeonRooms.Count);

                SimpleDungeonRoom connectedRoom = dungeonRooms[randomRoomIndex];

                //pick a random direction
                Direction randomDirection = GetRandomDirection();

                Vector2 newRoomLocation = GetNeighborLocation(connectedRoom, randomDirection);

                SimpleDungeonRoom existingRoom = GetRoom(newRoomLocation, dungeonRooms);

                if (existingRoom == null)
                {
                    //we found an empty spot, put the room here
                    newRoom.location = newRoomLocation;                    

                    AddExit(connectedRoom, newRoom, randomDirection);

                    dungeonRooms.Add(newRoom);

                    roomPlaced = true;
                }
            }
        }

        return dungeonRooms;

    }

    public void AddExit(SimpleDungeonRoom from, SimpleDungeonRoom to, Direction direction)
    {
        from.exits.Add(direction);

        switch (direction)
        {
            case Direction.East:
                to.exits.Add(Direction.West);
                break;
            case Direction.West:
                to.exits.Add(Direction.East);
                break;
            case Direction.South:
                to.exits.Add(Direction.North);
                break;
            case Direction.North:
                to.exits.Add(Direction.South);
                break;
            default:
                break;
        }
    }

    public SimpleDungeonRoom GetRoom(Vector2 roomLocation, List<SimpleDungeonRoom> allRooms)
    {
        SimpleDungeonRoom neighbor = allRooms.FirstOrDefault(r => r.location == roomLocation);

        return neighbor;
    }

    private static Vector2 GetNeighborLocation(SimpleDungeonRoom startingRoom, Direction direction)
    {
        Vector2 neighborLocation = startingRoom.location;

        switch (direction)
        {
            case Direction.East:
                neighborLocation.x++;
                break;
            case Direction.West:
                neighborLocation.x--;
                break;
            case Direction.North:
                neighborLocation.y++;
                break;
            case Direction.South:
                neighborLocation.y--;
                break;
            default:
                break;
        }
        return neighborLocation;
    }

    public Direction GetRandomDirection()
    {
        Array directions = Enum.GetValues(typeof(Direction));
        int randomIndex = UnityEngine.Random.Range(0, directions.Length);
        return (Direction)directions.GetValue(randomIndex);
    }    
}
