using UnityEngine;
using System.Collections;

public class KillMobEffect : GameEffect {

    public Animator deathAnimator;
    public BaseMovement movementToDisable;
    public Collider2D colliderToDisable;
    public Rigidbody2D rigidbodyToReset;

    public bool doDestroy = false;
    public float destroyDelay = 2.0f;

    public const string ANIM_PARAM_DEAD = "dead";

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (deathAnimator != null)
        {
            deathAnimator.SetBool(ANIM_PARAM_DEAD, true);
        }

        if (movementToDisable != null)
        {
            movementToDisable.movementDirection = Vector2.zero;
            movementToDisable.enabled = false;
        }

        if (colliderToDisable != null)
        {
            colliderToDisable.enabled = false;
        }

        if (rigidbodyToReset != null)
        {
            rigidbodyToReset.velocity = Vector2.zero;            
        }

        if (doDestroy)
        {
            try
            {
                Destroy(gameObject, destroyDelay);
            }
            catch { }
        }
    }

}
