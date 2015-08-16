using UnityEngine;
using System.Collections;

public class DropLootEffect : GameEffect {

    public LootHandler lootHandler;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (lootHandler == null)
        {
            lootHandler = gameObject.GetComponent<LootHandler>();
        }

        if (lootHandler != null)
        {
            lootHandler.DropLoot();
        }
    }
}
