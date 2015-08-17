using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomDoorsEffect : GameEffect {

    public List<GameObject> openDoors;
    public List<GameObject> closeDoors;

    public bool openCurrentRoomDoors = false;
    public bool closeCurrentRoomDoors = false;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
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
    }

    public void OpenDoor(GameObject door)
    {
        door.SetActive(false);
    }

    public void CloseDoor(GameObject door)
    {
        door.SetActive(true);
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
