using UnityEngine;
using System.Collections;

public class SetPlayerFacingEffect : GameEffect {

    public Vector2 desiredFacing = new Vector2(0.0f, -1.0f);

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        GameObject player = Globals.Instance.dungeonManager.player.gameObject;

        FacingHandler facingHandler = player.GetComponentInChildren<FacingHandler>();

        if (facingHandler != null)
        {
            facingHandler.facing = desiredFacing;
        }
    }
}
