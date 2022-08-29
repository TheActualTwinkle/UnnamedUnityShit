using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponToStandReturner : MonoBehaviour
{
    private Button button;
    private Stand stand;

    private void Start()
    {
        button = GetComponentInChildren<Button>(true);
        stand = GetComponentInParent<Stand>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p) && PlayerBack.CurrentWeapon != null && stand.WeaponOnStand == null)
            button.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
            button.gameObject.SetActive(false);
    }
}
