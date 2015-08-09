using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    private Vector2 cameraTarget;

    public Vector2 CameraTarget
    {
        get { return cameraTarget; }
        set
        {
            cameraTarget = value;
            cameraTarget.x += horizontalOffset;
            cameraTarget.y += verticalOffset;
        }
    }

    public Camera camera;

    public float transitionTime = 1.0f;

    public int horizontalOffset = 8;
    public int verticalOffset = 0;
    	
	void Awake () {
        if (camera == null)
        {
            camera = Camera.main;
        }

        CameraTarget = camera.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("DoTransitionCamera")]
    public void TransitionToTarget()
    {
        StartCoroutine(DoTransitionToTarget());
    }

    public void SnapToTarget()
    {
        Vector3 finalCamPosition = CameraTarget;
        finalCamPosition.z = camera.transform.position.z;

        camera.transform.position = finalCamPosition; 
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

            Vector3 newCamPosition = Vector3.Lerp(previousCameraPosition, CameraTarget, percentageComplete);

            newCamPosition.z = camera.transform.position.z;

            camera.transform.position = newCamPosition;
        }

        Vector3 finalCamPosition = CameraTarget;
        finalCamPosition.z = camera.transform.position.z;

        camera.transform.position = finalCamPosition;        
    }
}
