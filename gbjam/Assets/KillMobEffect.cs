using UnityEngine;
using System.Collections;

public class KillMobEffect : GameEffect {

    public Animator deathAnimator;
    public BaseMovement movementToDisable;

    public bool doDestroy = false;
    public float destroyDelay = 2.0f;

    public const string ANIM_PARAM_DEAD = "dead";

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll)
    {
        if (deathAnimator != null)
        {
            deathAnimator.SetBool(ANIM_PARAM_DEAD, true);
        }

        if (movementToDisable != null)
        {
            movementToDisable.enabled = false;
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
