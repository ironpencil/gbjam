using UnityEngine;
using System.Collections;

public class DelayInitialMovement : MonoBehaviour {

    public BaseMovement movementToDelay;

    public float secondsToDelay;

    private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
        movementToDelay.enabled = false;
        movementToDelay.disabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsToDelay)
        {
            movementToDelay.enabled = true;
            movementToDelay.disabled = false;

            this.enabled = false;
        }
	}
}
