using System.Collections;
using System;
using UnityEngine;

public abstract class Enemy : GameCharacter
{
    private float playerReceiveDmgRate = 2f;
    public float PlayerReceiveDmgRate { get => playerReceiveDmgRate; }

    protected PlayerController player;

    protected IEnumerator _doDamageEveryLoop;
    protected IEnumerator _attackPlayer;

    protected bool isContactsPlayer;

    protected bool canDealDamage = true;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    protected virtual IEnumerator DoDamageEveryLoop(float receiveRate, float enemyDamage)
    {
        canDealDamage = false;
        player.ReceiveDamage(enemyDamage);
        yield return new WaitForSeconds(receiveRate);
        canDealDamage = true;
    }

    protected virtual IEnumerator AttackPlayer(float attackDuration, float attackCooldown)
    {
        yield return null;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isContactsPlayer = true;
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isContactsPlayer = true;
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isContactsPlayer = false;
    }

    protected virtual void FollowTarget(GameObject target) { }

    protected virtual void ReturnToDefaultPosition(Vector3 defaultPos) { }

    protected virtual void ReturnToDefaultPosition(Vector2 defaultPos) { }
}
    
