using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private static Weapon currentWeapon;
    public static Weapon CurrentWeapon { get { return currentWeapon; } }

    [SerializeField] private float weaponFollowSpeedOffset;
    private float horizontalDistanseFromPlayer;
    private float verticalDistanseFromPlayer;

    [SerializeField] private float startTime;
    [SerializeField] private float shakeSpeed;
    private float currentTime;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        // Расстояние от игрока до его спины 
        horizontalDistanseFromPlayer = transform.localPosition.x;
        verticalDistanseFromPlayer = 0.17f;
        currentTime = startTime;
    }

    private void FixedUpdate()
    {
        KeepPlayerBackBehind();
        if (currentWeapon != null && Menus.IsGamePaused == false)
        {
            ShakeWeapon();
            WeaponToPlayerBack();
            ChangeWeaponSortingOrder();
        }
    }

    public static void SetNewPlayerWeapon(Weapon newWeapon)
    {
        if (CurrentWeapon != null)
            ResetPlayerCurrentWeapon(newWeapon);
        else
        {
            currentWeapon = newWeapon;
            currentWeapon.GetComponent<LayerSorter>().enabled = false;
            currentWeapon.transform.SetParent(null);
        }

    }

    public static void DropPlayerCurrentWeapon()
    {
        currentWeapon.transform.SetParent(null);
        currentWeapon = null;
    }

    // Тряска оружия по координате y
    private void ShakeWeapon()
    {
        // Половину времени наверх едем
        if (currentTime > startTime / 2)
        {
            currentTime -= Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, Time.fixedDeltaTime * shakeSpeed);
        }
        // Половину времени вниз едем
        else if (currentTime <= startTime / 2 && currentTime > 0)
        {
            currentTime -= Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up, Time.fixedDeltaTime * shakeSpeed);
        }
        // Если кд у таймера кончается то обновляем
        else
        {
            currentTime = startTime;
        }
    }

    // Оружие двигается к спине игрока.
    private void WeaponToPlayerBack()
    {
        var dist = Vector2.Distance(transform.position, currentWeapon.transform.position);

        currentWeapon.transform.position = Vector3.MoveTowards(currentWeapon.transform.position, transform.position, (player.Speed + dist - weaponFollowSpeedOffset) * Time.fixedDeltaTime);
    }

    // Метод для держания точки спины всегда за спиной игрока.
    private void KeepPlayerBackBehind()
    {

        if (Mathf.Abs(player.playerMovement.x) == 1)
        {
            transform.localPosition = new Vector3(horizontalDistanseFromPlayer * -player.playerMovement.x, transform.localPosition.y, transform.localPosition.z);
        }
        
        else if (Mathf.Abs(player.playerMovement.y) == 1)
        {
            transform.localPosition = new Vector3(verticalDistanseFromPlayer * player.playerMovement.y, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void ChangeWeaponSortingOrder()
    {
        var weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        var playerSprite = player.GetComponent<SpriteRenderer>();

        if (Mathf.Abs(player.playerMovement.x) == 1)
            weaponSprite.sortingOrder = playerSprite.sortingOrder;

        else if (Mathf.Abs(player.playerMovement.y) == 1)
            weaponSprite.sortingOrder = playerSprite.sortingOrder + (int)player.playerMovement.y;

        if (player.playerMovement.sqrMagnitude == 2)
            weaponSprite.sortingOrder = playerSprite.sortingOrder - 1;
    }


    private static void ResetPlayerCurrentWeapon(Weapon newWeapon)
    {
        DropPlayerCurrentWeapon();
        SetNewPlayerWeapon(newWeapon);
    }
}
