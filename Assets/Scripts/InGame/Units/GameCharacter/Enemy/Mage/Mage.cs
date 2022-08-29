using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class Mage : Enemy
{
    [SerializeField] private CircleCollider2D scanRange;    
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    
    [SerializeField] private Vector3 homePosition;
    private Vector2 mageMovement;

    [SerializeField] private float attackRange;
    private float attackDuration = 1f;
    private float attackCooldown = 1.5f;

    private bool canAttack;
    private bool isAttacking;
    private bool canMove;
    private bool isMoving;
    private bool isPlayerClose;

    private void Start()
    {
        canMove = true;
        canAttack = true;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        _doDamageEveryLoop = DoDamageEveryLoop(PlayerReceiveDmgRate, Damage);
        _attackPlayer = AttackPlayer(attackDuration, attackCooldown);
    }

    private void Update()
    {
        if (player != null)
        {
            if (isContactsPlayer == true && canDealDamage == true)
            {
                if (_doDamageEveryLoop != null)
                {
                    _doDamageEveryLoop = DoDamageEveryLoop(PlayerReceiveDmgRate, Damage);
                    StopCoroutine(_doDamageEveryLoop);
                }
                StartCoroutine(_doDamageEveryLoop);
            }

            SetMovement();

            SetAnimatorSettings();
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            MageMovment();
        }
    }

    protected override void FollowTarget(GameObject target)
    {
        if (canMove == true)
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (Speed) * Time.fixedDeltaTime);
        }
    }

    protected override void ReturnToDefaultPosition(Vector3 defaultPos)
    {
        if (canMove == true && Menus.IsGamePaused == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, defaultPos, (Speed) * Time.fixedDeltaTime);
            if (transform.position == homePosition)
            {
                isMoving = false;
            }
        }
    }

    private void MageMovment()
    {
        if (isPlayerClose == true && Menus.IsGamePaused == false)
        {
            if (IsPlayerInAttackRange() == false)
            {
                FollowTarget(player.gameObject);
            }

            else
            {
                isMoving = false;
                if (canAttack == true)
                {
                    if (_attackPlayer != null)
                    {
                        _attackPlayer = AttackPlayer(attackDuration, attackCooldown);
                        StopCoroutine(_attackPlayer);
                    }
                    StartCoroutine(_attackPlayer);
                }
            }
        }
        else
        {
            ReturnToDefaultPosition(homePosition);
        }
    }

    private float GetDistanceToPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance;
    }

    private bool IsPlayerInAttackRange()
    {
        if (GetDistanceToPlayer() <= attackRange)
            return true;
        else
            return false;
    }

    protected override IEnumerator AttackPlayer(float attackDuration, float attackCooldown)
    {
        isAttacking = true;
        canAttack = false;
        canMove = false;

        SpawnFireball();

        // Атакую пока идет attackDuration
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
        canMove = true;

        // Не разрешаю атаковать, пока идет attackCooldown
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    private void SpawnFireball()
    {
        // Spawn fireball 1 time
    }

    private void SetAnimatorSettings()
    {
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isPlayerClose", isPlayerClose);

        animator.SetFloat("MoveX", mageMovement.x);
        animator.SetFloat("MoveY", mageMovement.y);
    }

    private void SetMovement()
    {
        Vector3 distanceBtwPlayerAndMage = new Vector3();

        if (distanceBtwPlayerAndMage.x > 0)
            mageMovement.x = 1f;
        else if (distanceBtwPlayerAndMage.x < 0)
            mageMovement.x = -1f;

        if (distanceBtwPlayerAndMage.y > 0)
            mageMovement.y = 1f;
        else if (distanceBtwPlayerAndMage.y < 0)
            mageMovement.y = -1f;

        rigidbody2d.MovePosition(rigidbody2d.position + mageMovement.normalized * Speed * Time.fixedDeltaTime);
    }
}
