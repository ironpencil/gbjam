using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ChaseLootMovement : BaseMovement {

    public MoveTowardsTarget moveTowardsLoot;
    public BaseMovement moveNoLoot;

    public FacingHandler facingHandler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (moveDuringUpdate)
        {
            Move();
        }
	
	}

    public override void Move()
    {
        if (disabled) { return; }

        if (moveTowardsLoot.target == null || !moveTowardsLoot.target.gameObject.activeInHierarchy)
        {
            Lootable loot = FindLoot();
            if (loot != null)
            {
                moveTowardsLoot.target = loot.transform;
            }
            else
            {
                moveTowardsLoot.target = null;
            }
        }

        if (moveTowardsLoot.target == null )
        {
            moveTowardsLoot.target = null;
            moveTowardsLoot.movementDirection = Vector2.zero;
            moveTowardsLoot.disabled = true;

            facingHandler.movementController = moveNoLoot;
            moveNoLoot.disabled = false;
            moveNoLoot.Move();
        }
        else
        {
            moveNoLoot.movementDirection = Vector2.zero;
            moveNoLoot.disabled = true;

            facingHandler.movementController = moveTowardsLoot;
            moveTowardsLoot.disabled = false;
            moveTowardsLoot.Move();
        }
    }

    public Lootable FindLoot()
    {
        List<Lootable> loot = new List<Lootable>();

        DungeonRoom currentRoom = gameObject.GetComponentInParent<DungeonRoom>();

        if (currentRoom != null)
        {
            loot.AddRange(currentRoom.gameObject.GetComponentsInChildren<Lootable>());

            if (loot.Count > 0)
            {
                Debug.Log("Found some loot!");
            }

            //if (currentRoom.northDoor != null)
            //{
            //    doors.Add(currentRoom.northDoor);
            //}
            //if (currentRoom.southDoor != null)
            //{
            //    doors.Add(currentRoom.southDoor);
            //}
            //if (currentRoom.westDoor != null)
            //{
            //    doors.Add(currentRoom.westDoor);
            //}
            //if (currentRoom.eastDoor != null)
            //{
            //    doors.Add(currentRoom.eastDoor);
            //}
        }

        return loot.FirstOrDefault();
    }
}
