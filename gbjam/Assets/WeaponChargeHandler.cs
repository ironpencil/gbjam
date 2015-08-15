using UnityEngine;
using System.Collections;

public class WeaponChargeHandler : MonoBehaviour {

    public float weaponCharge = 0.0f;
    public float weaponChargeRate = 0.20f;

    public float maxWeaponCharge = 1.0f;

    public WeaponChargeBar weaponChargeBar;

    public bool fullyCharged = false;

	// Use this for initialization
	void Start () {
        //if (weaponChargeBar == null)
        //{
        //    weaponChargeBar = Globals.Instance.weaponChargeBar;
        //}
	
	}
	
	// Update is called once per frame
    void Update()
    {

        if (weaponCharge < maxWeaponCharge)
        {
            float newWeaponCharge = weaponCharge + (weaponChargeRate * Time.deltaTime);
            weaponCharge = Mathf.Min(maxWeaponCharge, newWeaponCharge);

            weaponChargeBar.SetChargeValue(weaponCharge);
        }

        fullyCharged = !(weaponCharge < maxWeaponCharge);
    }

    public void Attack()
    {
        weaponCharge = 0.0f;
        weaponChargeBar.SetChargeValue(0.0f);
    }

    public float GetChargePercent()
    {
        float chargePercent = 0.0f;

        if (fullyCharged)
        {
            chargePercent = 1.0f;
        }
        else
        {
            chargePercent = weaponCharge / maxWeaponCharge;
        }

        return chargePercent;
    }
}
