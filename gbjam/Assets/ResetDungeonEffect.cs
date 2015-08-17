using UnityEngine;
using System.Collections;

public class ResetDungeonEffect : GameEffect {
    
    public float resetDelay = 2.0f;

    public DungeonManager dungeonManager;

    public bool resetWholeDungeon = false;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (dungeonManager == null)
        {
            dungeonManager = (DungeonManager) FindObjectOfType(typeof(DungeonManager));
        }

        if (dungeonManager != null)
        {
            StartCoroutine(DoResetDungeon());
        }
    }

    public IEnumerator DoResetDungeon()
    {
        yield return new WaitForSeconds(resetDelay);

        if (resetWholeDungeon)
        {
            dungeonManager.ResetDungeon();
        }
        else
        {
            dungeonManager.ResetFloor();
        }
    }
}
