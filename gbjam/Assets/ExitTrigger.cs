using UnityEngine;
using System.Collections;

public class ExitTrigger : MonoBehaviour {

    public Direction exitDirection;
    public ExitHandler exitHandler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ExitTrigger:OnTriggerEnter2D");
        if (other.tag == "Player")
        {
            Debug.Log("Triggered by player!");
            //do the transition
            exitHandler.DoMoveInDirection(exitDirection);
        }
    }
}
