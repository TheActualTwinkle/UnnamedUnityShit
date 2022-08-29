
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{
    public static Vector3 RespawnPosition { get { return respawnPosition; } }
    private static Vector3 respawnPosition;

    private GameObject saveEffectPrefab;

    private void Start()
    {
        saveEffectPrefab = Resources.Load<GameObject>("UI/Particles/SaveEffect");
    }

    public static void SetRespawnPosition(Vector3 pos)
    {
        respawnPosition = pos;
    }

    public static void SetRespawnPosition(float[] pos)
    {
        respawnPosition.x = pos[0];
        respawnPosition.y = pos[1];
        respawnPosition.z = pos[2];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            respawnPosition = transform.position;
            p.Heal(p.MaxHp - p.CurrentHp);

            var saveEffect = Instantiate(saveEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(saveEffect, 4f);

            SaveLoadSystem.SavePlayerData(collision.GetComponent<PlayerController>());
        }
    }
}