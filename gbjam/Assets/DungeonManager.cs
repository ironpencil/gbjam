using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DungeonManager : MonoBehaviour {

    public DungeonGenerator dungeonGenerator;

    public DungeonRoom currentRoom;

    public List<DungeonRoom> allRooms = new List<DungeonRoom>();

    public CameraManager cameraManager;
    public GameObject playerPrefab;
    public Transform player;

    public Transform playerParent;

    public ScreenTransition screenTransition;

    public int currentFloor = 1;

	// Use this for initialization
	void Start () {
        //SetUpDungeon();
        ResetDungeon();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetUpDungeon();
        }
	
	}

    [ContextMenu("Set Up Dungeon")]
    public void SetUpDungeon()
    {
        allRooms.Clear();
                
        allRooms = dungeonGenerator.BuildDungeon();

        MoveToRoom(GetRoom(Vector2.zero), false); //don't spawn monsters in first room

        player.gameObject.SetActive(true);
        player.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y - 4.0f, currentRoom.transform.position.z);

        cameraManager.CameraTarget = currentRoom.transform.position;
        cameraManager.SnapToTarget();
        
    }

    public void MoveToRoom(DungeonRoom room, bool spawnMonsters)
    {
        currentRoom = room;

        SpawnerManager spawnManager = room.GetComponent<SpawnerManager>();
        
        if (spawnManager != null)
        {
            if (spawnMonsters)
            {
                spawnManager.SelectAndSpawnMonsters();
            }
            else
            {
                spawnManager.monstersSpawned = true; //flag so this room doesn't spawn monsters later
            }
        }
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

    public void MoveToNextFloor()
    {
        StartCoroutine(DoMoveToNextFloor());
    }

    private IEnumerator DoMoveToNextFloor()
    {
        //disable player object
        Globals.Instance.Pause(true);
        
        //TODO: do a screen transition
        yield return StartCoroutine(screenTransition.TransitionCoverScreen(1.0f));

        player.gameObject.SetActive(false);

        currentFloor++;

        SetUpDungeon();

        yield return null;

        yield return StartCoroutine(screenTransition.TransitionUncoverScreen(1.0f));
        
        Globals.Instance.Pause(false);

        //TODO: do screen transition in
    }

    public void ResetDungeon()
    {
        currentFloor = 1;

        if (player != null && player.gameObject != null)
        {
            DestroyImmediate(player.gameObject);
        }

        GameObject playerObject = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        playerObject.transform.parent = playerParent;

        player = playerObject.transform;

        dungeonGenerator.player = player.gameObject;

        SetUpDungeon();

    }
}
