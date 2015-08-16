using UnityEngine;
using System.Collections;

public class ModifySortingLayerEffect : GameEffect {

    public UpdateSortingLayer sortingLayerHandler;

    public int yOffsetAdjust = 5;


    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        sortingLayerHandler.yOffset += yOffsetAdjust;
    }
}
