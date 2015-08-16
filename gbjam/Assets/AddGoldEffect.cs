using UnityEngine;
using System.Collections;

public class AddGoldEffect : GameEffect {

    public int gpToAdd = 1;



    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        Globals.Instance.CurrentGP += gpToAdd;
    }
}
