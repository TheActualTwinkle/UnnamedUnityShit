using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public readonly float[] respawnPos;
    
    public readonly string savedScene;

    public readonly float damage;
    public readonly float maxHp;
    public readonly float currentHp;
    public readonly float maxMana;
    public readonly float currentMana;
    public readonly float speed;

    public PlayerData(PlayerController player)
    {
        respawnPos = new float[3];
        respawnPos[0] = RespawnPoint.RespawnPosition.x;
        respawnPos[1] = RespawnPoint.RespawnPosition.y;
        respawnPos[2] = RespawnPoint.RespawnPosition.z;

        savedScene = SceneManager.GetActiveScene().name;

        damage = player.Damage;
        maxHp = player.MaxHp;
        currentHp = player.CurrentHp;
        maxMana = player.MaxMana;
        currentMana = player.CurrentMana;
        speed = player.Speed;
    }
}
