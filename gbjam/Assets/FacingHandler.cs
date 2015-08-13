﻿using UnityEngine;
using System.Collections;

public class FacingHandler : MonoBehaviour {

    public BaseMovement movementController;
    public Animator animator;

    public const string ANIM_PARAM_FACING_X = "facingX";
    public const string ANIM_PARAM_FACING_Y = "facingY";
    public const string ANIM_PARAM_WALKING = "walking";

    public Vector2 facing = Vector2.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float moveX = movementController.movementDirection.x;
        float moveY = movementController.movementDirection.y;
        bool walking = false;

        if (Mathf.Abs(moveX) > 0 && Mathf.Abs(moveY) > 0)
        {
            //they're pressing both directions
            //so just use whatever our previous facing was

            walking = true;

            //TODO: make sure they haven't changed facing direction in one frame somehow
            //
        }
        else
        {
            // arbitrarily favor horizontal facing
            if (Mathf.Abs(moveX) > 0)
            {
                moveY = 0.0f;
                facing.x = moveX;
                facing.y = 0.0f;

                walking = true;
            }

            if (Mathf.Abs(moveY) > 0)
            {
                facing.x = 0.0f;
                facing.y = moveY;

                walking = true;
            }
        }

        animator.SetFloat(ANIM_PARAM_FACING_X, facing.x);
        animator.SetFloat(ANIM_PARAM_FACING_Y, facing.y);
        animator.SetBool(ANIM_PARAM_WALKING, walking);
        

        
        
	
	}
}
