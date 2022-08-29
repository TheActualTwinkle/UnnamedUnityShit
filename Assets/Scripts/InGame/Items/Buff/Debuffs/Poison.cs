using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Buff/Poison")]
public class Poison : Buff
{
    [Header("Poison-------------------------------------------------------------------------")]

    [SerializeField] private Color poisonColor = new Color(0.43f, 1f, 0.5f, 1f);
    private Color defaultColor = new Color(1f, 1f, 1f, 1f);

    private IEnumerator _doPoisonDamage;
    private IEnumerator _deactivateBuffOnTime;

    [SerializeField] private float damage;
    [SerializeField] private float speedSlow;
    [SerializeField] private float receiveDmgRate;

    public override void ActivateBuff(GameCharacter character, float currentDuration = 0f)
    {
        CurrentDuration = currentDuration;

        if (character.Buffs.Contains(this))
            TryDeactivateBuff(character);

        character.Buffs.Add(this);
        character.GetComponent<SpriteRenderer>().color = poisonColor;

        if (currentDuration == 0)
            character.DecreaseSpeed(speedSlow);

        if (_doPoisonDamage != null)
            character.StopCoroutine(_doPoisonDamage);

        if (_deactivateBuffOnTime != null)
            character.StopCoroutine(_deactivateBuffOnTime);

        _doPoisonDamage = DoPoisonDamage(character);
        _deactivateBuffOnTime = DeactivateBuffOnTime(character);

        character.StartCoroutine(_doPoisonDamage);
        character.StartCoroutine(_deactivateBuffOnTime);
    }

    public override bool TryDeactivateBuff(GameCharacter character)
    {
        if (character.Buffs.Contains(this))
        {
            character.StopCoroutine(_doPoisonDamage);
            character.StopCoroutine(_deactivateBuffOnTime);

            character.IncreaseSpeed(speedSlow);
            character.GetComponent<SpriteRenderer>().color = defaultColor;
            character.Buffs.Remove(this);

            CurrentDuration = 0;

            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator DoPoisonDamage(GameCharacter character)
    {                
        for (float i = CurrentDuration; i < Duration; i += receiveDmgRate)
        {
            character.ReceiveDamage(damage);
            yield return new WaitForSeconds(receiveDmgRate);
        }
    }

    private IEnumerator DeactivateBuffOnTime(GameCharacter character)
    {
        for (float currentDuration = CurrentDuration; currentDuration < Duration; currentDuration += Time.fixedDeltaTime)
        {
            CurrentDuration = currentDuration;
            yield return new WaitForFixedUpdate();
        }

        if (character != null)
        {
            TryDeactivateBuff(character);
            Debug.Log(BuffName + " is over");
        }
    }
}