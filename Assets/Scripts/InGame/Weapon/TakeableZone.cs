    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class TakeableZone : MonoBehaviour
{
    [SerializeField] private Button takeWeaponButton;
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p) && PlayerBack.CurrentWeapon != weapon)
            takeWeaponButton.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
            takeWeaponButton.gameObject.SetActive(false);
    }
}
