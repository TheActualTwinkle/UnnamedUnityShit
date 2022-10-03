using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(QuestReceiver), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : GameCharacter
{
    public static PlayerController Instance { get; private set; }
    public bool IsDashing { get => isDashing; }

    public float MaxMana { get => maxMana; }
    public float CurrentMana { get => currentMana; }

    [HideInInspector] public Vector2 playerMovement;
    [HideInInspector] public Vector2 playerLastMove;

    private Vector2 dashDirection;

    private IEnumerator _startDash;

    private Rigidbody2D rigidbody2d;
    private Animator animator;

    [SerializeField] private float maxMana;
    [SerializeField] private float currentMana;

    private float dashSpeed = 3.5f;
    private float dashCooldown = 0f;

    private static bool isDashing;
    private bool setDashAnimationTrigger;
    private bool isAttacking;
    private bool dashOnCooldown = false;
    private bool canPlayerAttack = true;

    private void Awake()
    {
        // TODELETE
        Keybinds.SetDefaultKeybinds();
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        if (NewGameTuner.IsNewGame == false)
            SetPlayerLoadedValues(this);
    }

    private void Update()
    {
        if (InteractAccessor.CanInteract)
        {
            playerMovement.x = Keybinds.GetAxisRaw(Axis.Horizontal);
            playerMovement.y = Keybinds.GetAxisRaw(Axis.Vertical);

            UpdatePlayerState();

            SetAnimatorSettings();
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    private void FixedUpdate()
    {
        if (InteractAccessor.CanInteract)
        {
            Move();
        }
    }

    protected override void Die()
    {
        CallDeathEvent();
        StartCoroutine(PlayerDie());
    }

    protected override void SetPlayerLoadedValues(PlayerController player)
    {
        base.SetPlayerLoadedValues(player);

        var playerData = SaveLoadSystem.LoadPlayerData();

        if (playerData != null)
        {
            player.maxMana = playerData.maxMana;
            player.currentMana = playerData.currentMana;
        }

        player.transform.position = RespawnPoint.RespawnPosition;
    }

    protected override void Respawn(GameObject prefab)
    {
        base.Respawn(prefab);

        currentMana = maxMana;
    }

    private void UpdatePlayerState()
    {
        if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Dash]) && dashOnCooldown == false && playerMovement.sqrMagnitude != 0f)
        {
            if (_startDash != null)
                StopCoroutine(_startDash);

            _startDash = StartDash();
            StartCoroutine(_startDash);
        }

        if (Input.GetKeyDown(Keybinds.KeyBinds[Actions.Attack1]) && canPlayerAttack == true)
        {
            // Coroutine needed
            Attack();
        }
    }

    private void SetAnimatorSettings()
    {
        animator.SetFloat("Horizontal", playerMovement.x);
        animator.SetFloat("Vertical", playerMovement.y);

        animator.SetFloat("Speed", playerMovement.sqrMagnitude * Speed);

        if (setDashAnimationTrigger)
        {
            animator.SetTrigger("Dash");
            setDashAnimationTrigger = false;
        }

        if (playerMovement.x < -0.5f || playerMovement.x > 0.5f)
        {
            playerLastMove.x = playerMovement.x;
            animator.SetFloat("LastMoveX", playerLastMove.x);
            animator.SetFloat("LastMoveY", 0f);

        }
        if (playerMovement.y < -0.5f || playerMovement.y > 0.5f)
        {
            playerLastMove.y = playerMovement.y;
            animator.SetFloat("LastMoveY", playerLastMove.y);
            animator.SetFloat("LastMoveX", 0f);
        }

        animator.speed = Speed / 6;
    }

    private void Move()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + playerMovement.normalized * Speed * Time.fixedDeltaTime);
    }

    private void Dash(Vector2 direction)
    {
        rigidbody2d.velocity = direction.normalized * dashSpeed * Speed;
    }

    private void Attack()
    {
        // Some code here.
    }

    private IEnumerator StartDash()
    {
        isDashing = true;
        setDashAnimationTrigger = true;
        dashOnCooldown = true;

        dashDirection = playerMovement.normalized;

        yield return new WaitForSeconds(dashCooldown);

        dashOnCooldown = false;
    }

    private IEnumerator PlayerDie()
    {
        Debug.Log("Player is dead");

        RemoveAllBuffs();

        if (PlayerBack.CurrentWeapon != null)
        {
            // Weapon save stuff should be here.
            Debug.Log(PlayerBack.CurrentWeapon.name + " was player weapon");
        }

        Instance = null;

        // Dead ainmation should be here.
        yield return new WaitForSeconds(2f);

        Respawn(Resources.Load<GameObject>("Player/Player"));
        SceneChanger.ReloadScene();
    }

    // Animator.
    private void OnDashDelayEnded()
    {
        Dash(dashDirection);
    }

    // Animator.
    private void OnDashEnded()
    {
        isDashing = false;
        rigidbody2d.velocity = Vector2.zero;
    }
}

