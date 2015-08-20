using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRandomObjectEffect : GameEffect {

    public List<GameObject> possibleObjects;

    public bool doScatter = true;

    public Vector2 scatterDirection = new Vector2(6.0f, 6.0f);

    public Vector2 spawnOffset = new Vector2(0.0f, 8.0f);

    public float scatterForce = 500.0f;

    public Transform objectParent;

    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (possibleObjects != null && possibleObjects.Count > 0)
        {
            if (objectParent == null)
            {
                objectParent = transform.parent;
            }

            Vector2 dropPos = (Vector2)transform.position + spawnOffset;

            int randomIndex = Random.Range(0, possibleObjects.Count);

            GameObject spawnedObj = (GameObject)GameObject.Instantiate(possibleObjects[randomIndex], dropPos, transform.rotation);

            spawnedObj.transform.parent = objectParent;

            DelayInitialMovement delayMovement = spawnedObj.GetComponent<DelayInitialMovement>();

            if (delayMovement != null)
            {
                delayMovement.secondsToDelay = 0.5f;
            }

            TemporaryInvulnEffect invulnEffect = spawnedObj.GetComponent<TemporaryInvulnEffect>();

            if (invulnEffect != null)
            {
                invulnEffect.ActivateEffect(null, 0.0f, null, null);
            }

            if (doScatter)
            {
                Rigidbody2D lootRB = spawnedObj.GetComponent<Rigidbody2D>();

                int randomPosX = Random.Range((int)scatterDirection.x * -1, (int)scatterDirection.x + 1);
                int randomPosY = Random.Range((int)scatterDirection.y * -1, (int)scatterDirection.y + 1);

                dropPos.x += randomPosX;
                dropPos.y += randomPosY;

                spawnedObj.transform.position = dropPos;

                lootRB.AddRelativeForce(new Vector2(randomPosX, randomPosY) * scatterForce, ForceMode2D.Impulse);
            }
        }
    }
}
