using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DungeonManager : MonoBehaviour {

    public DungeonGenerator dungeonGenerator;

    public DungeonRoom currentRoom;

    public List<DungeonRoom> allRooms = new List<DungeonRoom>();

    public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("Set Up Dungeon")]
    public void SetUpDungeon()
    {
        allRooms.Clear();
        
        allRooms = dungeonGenerator.BuildDungeon();

        currentRoom = GetRoom(Vector2.zero);

        player.position = currentRoom.transform.position;
    }

    private DungeonRoom GetRoom(Vector2 roomLocation)
    {
        DungeonRoom room = allRooms.FirstOrDefault(r => r.roomCoordinates == roomLocation);

        return room;
    }

    public DungeonRoom GetRoomInDirection(DungeonRoom fromRoom, Direction direction)
    {
        Vector2 toRoomLocation = fromRoom.roomCoordinates;

        switch (direction)
        {
            case Direction.East:
                toRoomLocation.x++;
                break;
            case Direction.West:
                toRoomLocation.x--;
                break;
            case Direction.North:
                toRoomLocation.y++;
                break;
            case Direction.South:
                toRoomLocation.y--;
                break;
            default:
                break;
        }

        DungeonRoom toRoom = GetRoom(toRoomLocation);

        return toRoom;
    }
}
