using UnityEngine;
using System.Collections;

public class BounceFromCollisionEffect : GameEffect
{
    public BaseMovement movementToDisable;
    public Rigidbody2D thisRigidbody;
    public Collider2D thisCollider;

    public float bounceMagnitude = 100.0f;
    public float movementDisabledTime = 0.25f;

    public bool useColliderAsCenter = true;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {

        if (coll == null && other == null) { return; }

        movementToDisable.enabled = false;

        Vector2 direction = Vector2.zero;
        Vector2 bounceFromPoint = Vector2.zero;

        if (coll != null)
        {

            bounceFromPoint = coll.contacts[0].point;

            
        }
        else 
        {
            //we don't have a collision object, so just move away from the collider
            bounceFromPoint = (Vector2) other.transform.position + other.offset;
        }

        Vector2 bouncePoint = transform.position;

        if (useColliderAsCenter)
        {
            //find this collider's center            
            bouncePoint = (Vector2) transform.position + thisCollider.offset;
        }

        direction = (bouncePoint - bounceFromPoint).normalized;

        Debug.Log("Contact! Bounce direction = " + direction.ToString());

        thisRigidbody.AddForce(direction * bounceMagnitude, ForceMode2D.Impulse);

        StartCoroutine(EnableMovement());
    }

    public IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(movementDisabledTime);

        movementToDisable.enabled = true;
    }

}
