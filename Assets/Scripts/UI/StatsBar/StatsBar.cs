using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image manaBar;

    [Range(0.01f, 0.1f)]
    [SerializeField] private float fillChangeSpeed;

    public void Update()
    {
        if (PlayerController.Instance != null)
        {
            if (hpBar.fillAmount != PlayerController.Instance.CurrentHp / PlayerController.Instance?.MaxHp)
                UpdateHpBar();

            if (manaBar.fillAmount != PlayerController.Instance.CurrentMana / PlayerController.Instance?.MaxMana)
                UpdateManaBar();
        }        
    }

    private void UpdateHpBar()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, PlayerController.Instance.CurrentHp / PlayerController.Instance.MaxHp, fillChangeSpeed);
        hpBar.color = Color.Lerp(endColor, startColor, (PlayerController.Instance.CurrentHp / PlayerController.Instance.MaxHp));
    }

    private void UpdateManaBar()
    {
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, PlayerController.Instance.CurrentHp / PlayerController.Instance.MaxHp, fillChangeSpeed);
    }
}
