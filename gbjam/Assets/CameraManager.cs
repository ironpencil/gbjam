using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Transform cameraTarget;

    public Camera camera;

    public float transitionTime = 1.0f;
    
	// Use this for initialization
	void Start () {
        if (camera == null)
        {
            camera = Camera.main;
        }

        cameraTarget = camera.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("DoTransitionCamera")]
    public void TransitionToTarget()
    {
        StartCoroutine(DoTransitionToTarget());
    }
    
    private IEnumerator DoTransitionToTarget()
    {
        float elapsedTime = 0.0f;
        float currentTime = Time.realtimeSinceStartup;

        Vector2 previousCameraPosition = camera.transform.position;

        while (elapsedTime < transitionTime)
        {
            yield return null;

            float realDeltaTime = Time.realtimeSinceStartup - currentTime;
            currentTime = Time.realtimeSinceStartup;

            elapsedTime += realDeltaTime;

            float percentageComplete = elapsedTime / transitionTime;

            Vector3 newCamPosition = Vector3.Lerp(previousCameraPosition, cameraTarget.position, percentageComplete);

            newCamPosition.z = camera.transform.position.z;

            camera.transform.position = newCamPosition;
        }

        Vector3 finalCamPosition = cameraTarget.position;
        finalCamPosition.z = camera.transform.position.z;

        camera.transform.position = finalCamPosition;        
    }
}
