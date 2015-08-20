using UnityEngine;
using System.Collections;

public class StopMusicEffect : GameEffect {


    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        AudioManager.Instance.StopMusic();
    }
}
