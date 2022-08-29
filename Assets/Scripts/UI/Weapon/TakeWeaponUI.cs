using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeWeaponUI : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    // Button
    private void TakeWeapon()
    {
        var stand = weapon.GetComponentInParent<Stand>();
        if (stand != null)
            stand.TakeWeaponFromStand();
        else
            PlayerBack.SetNewPlayerWeapon(weapon);
    }
}
