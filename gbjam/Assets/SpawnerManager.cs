using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour {

    public List<GameObject> availableSpawnPatterns = new List<GameObject>();

    public bool monstersSpawned = false;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectAndSpawnMonsters()
    {
        if (!monstersSpawned)
        {
            int randomSpawner = Random.Range(0, availableSpawnPatterns.Count);

            GameObject spawner = availableSpawnPatterns[randomSpawner];

            spawner = (GameObject)GameObject.Instantiate(spawner, transform.position, Quaternion.identity);

            spawner.transform.parent = transform;
        }

        monstersSpawned = true;
    }
}
