using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColliderEffectTrigger : MonoBehaviour {

    public List<GameEffect> onTriggerEffects;

    public bool destroyAfterTrigger = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameEffect effect in onTriggerEffects)
        {
            effect.ActivateEffect(gameObject, 0.0f, null, other);
        }

        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }
}
