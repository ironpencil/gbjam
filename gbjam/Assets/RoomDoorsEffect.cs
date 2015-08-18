using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomDoorsEffect : GameEffect {


    public List<GameObject> openDoors;
    public List<GameObject> closeDoors;

    public bool openCurrentRoomDoors = false;
    public bool closeCurrentRoomDoors = false;

    public SoundEffectHandler doorSound;

    private bool doorChanged = false;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        doorChanged = false;
        if (openCurrentRoomDoors || closeCurrentRoomDoors)
        {
            List<GameObject> roomDoors = GetCurrentRoomDoors();

            if (openCurrentRoomDoors)
            {
                openDoors.AddRange(roomDoors);
            }

            if (closeCurrentRoomDoors)
            {
                closeDoors.AddRange(roomDoors);
            }
        }

        foreach (GameObject door in openDoors)
        {
            OpenDoor(door);
        }

        foreach (GameObject door in closeDoors)
        {
            CloseDoor(door);
        }

        if (doorChanged && doorSound != null)
        {
            doorSound.PlayEffect();
        }
    }

    public void OpenDoor(GameObject door)
    {
        door.SetActive(false);
        doorChanged = true;
    }

    public void CloseDoor(GameObject door)
    {
        door.SetActive(true);
        doorChanged = true;
    }

    public List<GameObject> GetCurrentRoomDoors()
    {
        List<GameObject> doors = new List<GameObject>();

        DungeonRoom currentRoom = gameObject.GetComponentInParent<DungeonRoom>();

        if (currentRoom != null)
        {
            if (currentRoom.northDoor != null)
            {
                doors.Add(currentRoom.northDoor);
            }
            if (currentRoom.southDoor != null)
            {
                doors.Add(currentRoom.southDoor);
            }
            if (currentRoom.westDoor != null)
            {
                doors.Add(currentRoom.westDoor);
            }
            if (currentRoom.eastDoor != null)
            {
                doors.Add(currentRoom.eastDoor);
            }
        }

        return doors;
    }
}
