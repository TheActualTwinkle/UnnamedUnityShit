using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stand : MonoBehaviour
{
    private Weapon weaponOnStand;
    public Weapon WeaponOnStand { get { return weaponOnStand; } }
    [SerializeField] private GameObject weaponPlace;

    private void Start()
    {
        weaponOnStand = GetComponentInChildren<Weapon>();
    }

    private void FixedUpdate()
    {
        if (weaponOnStand != null)
            MoveWeaponToStand();
    }

    // Button.
    private void SetWeaponToStand()
    {
        Weapon weapon = PlayerBack.CurrentWeapon;
        PlayerBack.DropPlayerCurrentWeapon();

        SetUpWeaponOnStand(weapon);
    }

    // Button.
    public void TakeWeaponFromStand()
    {
        // Takeable Zone делаю child оружия
        TakeableZone weaponTakeableZone = gameObject.GetComponentInChildren<TakeableZone>();
        weaponTakeableZone.transform.SetParent(weaponOnStand.transform);
        weaponTakeableZone.transform.localPosition = Vector3.zero;

        if (PlayerBack.CurrentWeapon != null)
        {
            Weapon playerWeapon = PlayerBack.CurrentWeapon;
            PlayerBack.SetNewPlayerWeapon(weaponOnStand);
            SetUpWeaponOnStand(playerWeapon);
        }
        else
        {
            PlayerBack.SetNewPlayerWeapon(weaponOnStand);
            weaponOnStand = null;
        }
    }

    private void SetUpWeaponOnStand(Weapon weapon)
    {
        weaponOnStand = weapon;
        weaponOnStand.GetComponent<LayerSorter>().enabled = true;

        // Takeable Zone делаю child стенда
        TakeableZone weaponTakeableZone = weaponOnStand.GetComponentInChildren<TakeableZone>();
        weaponTakeableZone.transform.SetParent(transform);
        weaponTakeableZone.transform.localPosition = Vector3.zero;

        weaponOnStand.transform.SetParent(weaponPlace.transform);
    }

    private void MoveWeaponToStand()
    {
        weaponOnStand.transform.position = Vector3.MoveTowards(weaponOnStand.transform.position, weaponPlace.transform.position, 5f * Time.fixedDeltaTime);
    }
}
