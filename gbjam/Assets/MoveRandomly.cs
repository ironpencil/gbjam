﻿using UnityEngine;
using System.Collections;

public class MoveRandomly : BaseMovement {

    public float minMoveTime = 1.0f;
    public float maxMoveTime = 2.0f;

    public float minMoveDelay = 1.0f;
    public float maxMoveDelay = 2.0f;

    private float moveStartTime = 0.0f; 
    private float moveEndTime = 0.0f;

    private bool isMoving = false;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving && Time.time > moveEndTime)
        {
            //we are moving, and we should stop
            StopMoving();
        }
        else if (!isMoving && Time.time > moveStartTime)
        {
            //we are not moving, and we should start
            StartMoving();
        }
        
	
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

    private void StartMoving()
    {
        movementDirection = new Vector2();
        //movementDirection.x = Random.Range(-1, 2);
        //movementDirection.y = Random.Range(-1, 2);
        movementDirection.x = Mathf.Clamp(Random.Range(-2.0f, 2.0f), -1.0f, 1.0f);
        movementDirection.y = Mathf.Clamp(Random.Range(-2.0f, 2.0f), -1.0f, 1.0f);
        isMoving = true;
        moveEndTime = Time.time + Random.Range(minMoveTime, maxMoveTime);
    }

    private void StopMoving()
    {
        movementDirection = Vector2.zero;
        isMoving = false;
        moveStartTime = Time.time + Random.Range(minMoveDelay, maxMoveDelay);
    }


}
