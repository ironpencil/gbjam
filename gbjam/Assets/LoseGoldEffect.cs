using UnityEngine;
using System.Collections;

public class LoseGoldEffect : GameEffect {

    public float loseGoldPercent = 0.5f;



    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        int goldLost = (int) (Globals.Instance.CurrentGP * loseGoldPercent);

        Globals.Instance.CurrentGP -= goldLost;
    }
}
