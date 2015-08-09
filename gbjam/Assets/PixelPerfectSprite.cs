using UnityEngine;
using System.Collections;

public class PixelPerfectSprite : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector2 position = transform.position;

        position.x = Mathf.RoundToInt(position.x);
        position.y = Mathf.RoundToInt(position.y);

        transform.position = position;
    }
}
