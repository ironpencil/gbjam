using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public EffectSource damageType;

    public float damageValue = 10.0f;

    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;

    private float lastAttackTime = 0.0f;

    private bool doDealDamage = false;
    public Collider2D weaponCollider;

    private bool attackComplete = false;

	// Use this for initialization
	void Start () {
        //weaponCollider = gameObject.GetComponent<Collider2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (attackComplete)
        {
            attackComplete = false;
            gameObject.SetActive(false);
        }
	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {        
        TakesDamage takesDamage = other.gameObject.GetComponent<TakesDamage>();

        if (takesDamage != null)
        {
            takesDamage.ApplyDamage(damageValue, damageType, null);
        }
    }

    public void Attack()
    {
        if (Time.time > lastAttackTime + attackDelay)
        {
            //we can attack
            gameObject.SetActive(true);
            StartCoroutine(DoAttack());
            lastAttackTime = Time.time;
        }
    }

    public IEnumerator DoAttack()
    {
        attackComplete = false;

        doDealDamage = true;
        weaponCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        doDealDamage = false;
        weaponCollider.enabled = false;

        attackComplete = true;
    }
}
