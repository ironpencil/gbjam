using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {

    public GameObject monsterPrefab;

    public Transform enemyParent;

	// Use this for initialization
	void Start () {
        GameObject monster = (GameObject) GameObject.Instantiate(monsterPrefab, transform.position, Quaternion.identity);

        if (enemyParent == null)
        {
            enemyParent = transform.parent;
        }

        monster.transform.parent = enemyParent;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
