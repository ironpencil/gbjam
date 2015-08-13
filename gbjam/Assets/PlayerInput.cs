using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public BaseMovement movementController;
    public FacingHandler facingHandler;
    public WeaponHandler weaponHandler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        HandleMovement();
        HandleAttack();
	
	}

    private void HandleAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            weaponHandler.Attack(facingHandler.facing);
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        float horizontalMovement = horizontal;

        float vertical = Input.GetAxisRaw("Vertical");

        float verticalMovement = vertical;

        movementController.movementDirection = new Vector2(horizontalMovement, verticalMovement);
    }
}
