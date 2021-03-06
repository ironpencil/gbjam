﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonRoom : MonoBehaviour {

    public GameObject northExit;
    public GameObject southExit;
    public GameObject westExit;
    public GameObject eastExit;

    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject westDoor;
    public GameObject eastDoor;

    public HashSet<Direction> exits;

    public Vector2 roomCoordinates;

    public SpawnerManager spawnerManager;

    public StairTrigger stairsDown;
    public StairTrigger stairsUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
