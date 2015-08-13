using UnityEngine;
using System.Collections;

public class ResetDungeonEffect : GameEffect {
    
    public float resetDelay = 2.0f;

    public DungeonManager dungeonManager;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll)
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

        dungeonManager.ResetDungeon();
    }
}
