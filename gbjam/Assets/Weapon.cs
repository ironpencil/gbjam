﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public WeaponChargeHandler weaponChargeHandler;

    public EffectSource damageType;

    public float baseDamage = 2.0f;
    public float chargeBasedDamage = 8.0f;    
    public float bonusChargedDamage = 10.0f;
    public float actualDamage = 0.0f;

    public float chargeRate = 0.2f;

    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;    

    private float lastAttackTime = 0.0f;

    private bool doDealDamage = false;
    public Collider2D weaponCollider;

    private bool attackComplete = false;

    public Animator attackerAnimator;

    private const string ANIM_PARAM_ATTACKING = "attacking";

    [SerializeField]
    private bool equipped = false;
    public bool Equipped
    {
        get
        {
            return equipped;
        }
        set
        {
            equipped = value;            
        }
    }


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
            Collider2D thisCollider = gameObject.GetComponent<Collider2D>();
            takesDamage.ApplyDamage(actualDamage, damageType, null, thisCollider);
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

        if (weaponChargeHandler.fullyCharged)
        {
            actualDamage = baseDamage + chargeBasedDamage + bonusChargedDamage;
        }
        else
        {
            float chargePercent = weaponChargeHandler.GetChargePercent();
            actualDamage = baseDamage + (chargeBasedDamage * chargePercent);
        }

        if (attackerAnimator != null)
        {
            attackerAnimator.SetBool(ANIM_PARAM_ATTACKING, true);
        }

        weaponChargeHandler.Attack();

        doDealDamage = true;
        weaponCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        doDealDamage = false;
        weaponCollider.enabled = false;

        attackerAnimator.SetBool(ANIM_PARAM_ATTACKING, false);
        attackComplete = true;
    }
}
