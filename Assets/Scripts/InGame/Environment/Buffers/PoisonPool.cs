using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    [SerializeField] private List<Buff> buffs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out GameCharacter character))
        {
            var saveEffectPrefab = Resources.Load<GameObject>("UI/Particles/SaveEffect");
            var saveEffect = Instantiate(saveEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(saveEffect, 4f);

            foreach (Buff buff in buffs)
            {
                character.AddBuff(buff);
            }
        }
    }

}
