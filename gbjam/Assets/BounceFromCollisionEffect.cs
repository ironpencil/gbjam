using UnityEngine;
using System.Collections;

public class BounceFromCollisionEffect : GameEffect
{
    public BaseMovement movementToDisable;
    public Rigidbody2D rb;

    public float bounceMagnitude = 100.0f;
    public float movementDisabledTime = 0.25f;

    public bool useColliderAsCenter = true;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll)
    {

        if (coll == null) { return; }

        movementToDisable.enabled = false;

        Vector2 contactPoint = coll.contacts[0].point;

        Vector2 bouncePoint = transform.position;

        if (useColliderAsCenter)
        {
            //find this collider's center
            Collider2D thisCollider = coll.collider;
            bouncePoint = (Vector2)transform.position + thisCollider.offset;
        }

        Vector2 direction = (bouncePoint - contactPoint).normalized;

        Debug.Log("Contact! Bounce direction = " + direction.ToString());

        rb.AddForce(direction * bounceMagnitude, ForceMode2D.Impulse);

        StartCoroutine(EnableMovement());
    }

    public IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(movementDisabledTime);

        movementToDisable.enabled = true;
    }

}
