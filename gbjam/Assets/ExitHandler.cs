using UnityEngine;
using System.Collections;

public class ExitHandler : MonoBehaviour {

    public ExitTrigger northExit;
    public ExitTrigger southExit;
    public ExitTrigger westExit;
    public ExitTrigger eastExit;
    
    public DungeonManager dungeonManager;
    public CameraManager cameraManager;
    public GameObject player;

    public float roomTransitionTime = 1.0f;

    public int playerExitPlacementOffset = 2;
    public int playerExitSouthOffset = 8;

    

    public bool doneMoving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (doneMoving)
        {
            Time.timeScale = 1.0f;
            doneMoving = false;            
        }
	}

    public void DoMoveInDirection(Direction direction)
    {

        DungeonRoom newRoom = dungeonManager.GetRoomInDirection(dungeonManager.currentRoom, direction);

        if (newRoom == null) { return; }

        Time.timeScale = 0.0f;

        cameraManager.transitionTime = roomTransitionTime;

        cameraManager.CameraTarget = newRoom.transform.position;

        Vector2 newPlayerPosition = player.transform.position;


        switch (direction)
        {
            case Direction.East:
                newPlayerPosition.x = newRoom.westExit.transform.position.x + playerExitPlacementOffset;
                break;
            case Direction.West:
                newPlayerPosition.x = newRoom.eastExit.transform.position.x - playerExitPlacementOffset;
                break;
            case Direction.North:
                newPlayerPosition.y = newRoom.southExit.transform.position.y + playerExitPlacementOffset;
                break;
            case Direction.South:
                newPlayerPosition.y = newRoom.northExit.transform.position.y - playerExitPlacementOffset - playerExitSouthOffset;
                break;
            default:
                break;
        }

        MovePlayerToNewRoom(newPlayerPosition);
        cameraManager.TransitionToTarget();

        dungeonManager.MoveToRoom(newRoom, true);

        //dungeonManager.currentRoom = newRoom;        
    }
    
    public void MovePlayerToNewRoom(Vector2 targetPosition)
    {
        StartCoroutine(DoMovePlayerToNewRoom(targetPosition));
    }

    private IEnumerator DoMovePlayerToNewRoom(Vector2 targetPosition)
    {
        float elapsedTime = 0.0f;
        float currentTime = Time.realtimeSinceStartup;

        Vector2 previousPosition = player.transform.position;

        while (elapsedTime < roomTransitionTime)
        {
            yield return null;

            float realDeltaTime = Time.realtimeSinceStartup - currentTime;
            currentTime = Time.realtimeSinceStartup;

            elapsedTime += realDeltaTime;

            float percentageComplete = elapsedTime / roomTransitionTime;

            Vector3 newPosition = Vector3.Lerp(previousPosition, targetPosition, percentageComplete);

            player.transform.position = newPosition;
        }

        player.transform.position = targetPosition;

        doneMoving = true;
    }


}
