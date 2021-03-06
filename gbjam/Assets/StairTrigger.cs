﻿using UnityEngine;
using System.Collections;

public class StairTrigger : MonoBehaviour
{
    public DungeonManager dungeonManager;

    public enum StairDirection
    {        
        Down,
        Up
    }

    public StairDirection stairDirection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("StairTrigger:OnTriggerEnter2D");
        if (other.tag == "Player")
        {
            Debug.Log("Triggered by player!");
            //do the transition
            if (stairDirection == StairDirection.Up)
            {
                dungeonManager.MoveToNextFloor();
            }
        }
    }
}
