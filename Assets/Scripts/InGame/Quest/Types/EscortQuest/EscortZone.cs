using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class EscortZone : MonoBehaviour
{
    public static EventHandler<EscortQuestInfo> EscortedEvent;

    [SerializeField] private EscortQuest quest;
    public EscortQuest Quest => quest;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            EscortedEvent?.Invoke(this, new EscortQuestInfo(unit, transform.position, SceneManager.GetActiveScene().name));
        }
    }

    private void OnValidate()
    {
        if (quest != null)
        {
            transform.position = quest.WhereToEscort;
        }
    }
}
