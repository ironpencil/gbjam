using UnityEngine;
using System.Collections;

public class TopDown2DController : BaseMovement {

    Rigidbody2D rb;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();               
	}
	
	// Update is called once per frame
	void Update () {

        //float horizontal = Input.GetAxisRaw("Horizontal");

        //float horizontalMovement = horizontal;

        //float vertical = Input.GetAxisRaw("Vertical");

        //float verticalMovement = vertical;

        //movementDirection = new Vector2(horizontalMovement, verticalMovement);





        //Vector2 position = transform.position;

        //position.x += horizontalMovement;
        //position.y += verticalMovement;

        //transform.position = position;        
	}

    void FixedUpdate()
    {
        Vector2 velocity = movementDirection * speed;        
        rb.velocity = velocity;

        //rb.AddRelativeForce(velocity);

        //if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        //{
        //    rb.velocity = rb.velocity.normalized * maxSpeed;
        //}

    }
}
