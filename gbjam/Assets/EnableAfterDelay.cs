using UnityEngine;
using System.Collections;

public class EnableAfterDelay : MonoBehaviour {

    public MonoBehaviour scriptToEnable;

    public float enableDelay = 3.0f;

    private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (scriptToEnable != null)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > enableDelay)
            {
                scriptToEnable.enabled = true;
            }
        }
	}
}
