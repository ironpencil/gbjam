using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {

    public Weapon currentWeapon;
    public WeaponChargeHandler chargeHandler;

	// Use this for initialization
	void Start () {
        //currentWeapon.weaponChargeHandler.weaponChargeBar = Globals.Instance.weaponChargeBar;
        currentWeapon.Equipped = true;
        chargeHandler.weaponChargeRate = currentWeapon.chargeRate;
        chargeHandler.weaponChargeBar = Globals.Instance.weaponChargeBar;
        currentWeapon.weaponChargeHandler = chargeHandler;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Attack(Vector2 facing)
    {
        Vector3 rotation = new Vector3();

        if (facing.x > 0)
        {
            rotation.z = 0;
        }
        else if (facing.x < 0)
        {
            rotation.z = 180;
        }
        else if (facing.y > 0)
        {
            rotation.z = 90;
        }
        else
        {
            rotation.z = -90;
        }

        transform.localEulerAngles = rotation;

        currentWeapon.Attack();
    }
}
