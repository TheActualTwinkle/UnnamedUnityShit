using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Renderer), typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private string unitName;
    public string UnitName { get => unitName; }

    [SerializeField] private float damage;
    public float Damage { get => damage; }

    [SerializeField] private float maxHp;
    public float MaxHp { get => maxHp; }

    [SerializeField] private float currentHp;
    public float CurrentHp { get => currentHp; }

    [SerializeField] private float speed;
    public float Speed { get => speed; }

    public readonly float minSpeed = 1f;

    public static event Action<Unit> DeathEvent;
    public static event Action<float> DamageReceivedEvent;

    private void OnValidate()
    {
        unitName = name;
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    // Получение урона по GameObject
    public virtual void ReceiveDamage(float damage)
    {
        if (damage <= 0)
            return;

        currentHp -= damage;

        DamageReceivedEvent?.Invoke(damage);

        if (currentHp <= 0)
            Die();
    }

    // Востановление хп юнита
    public virtual void Heal(float healAmount)
    {
        currentHp += healAmount;
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    } 

    public void DecreaseSpeed(float amount)
    {
        if (amount >= speed - minSpeed)
            speed = minSpeed;
        else
            speed -= amount;
    }
    
    protected virtual void Die()
    {
        DeathEvent?.Invoke(this);

        try
        {
            Destroy(gameObject);
        }
        catch
        {
            Debug.LogError(gameObject.name + " is dead");
        }
    }

    protected virtual void Respawn(GameObject prefab)
    {
        Destroy(gameObject);
        Instantiate(prefab, RespawnPoint.RespawnPosition, Quaternion.identity);

        currentHp = maxHp;
    }

    // FIXME!!
    protected virtual void SetPlayerLoadedValues(PlayerController player)
    {
        var playerData = SaveLoadSystem.LoadPlayerData();

        if (playerData != null)
        {
            player.damage = playerData.damage;
            player.currentHp = playerData.currentHp;
            player.maxHp = playerData.maxHp;
            player.speed = playerData.speed;
        }
    }

    protected void CallDeathEvent()
    {
        DeathEvent?.Invoke(this);
    }
}
