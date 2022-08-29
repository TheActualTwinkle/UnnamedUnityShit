using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectLookToMouse : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Camera mainCamera;
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        mainCamera = FindObjectOfType<Camera>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowTarget(player.gameObject);
        LookAtMouse();
    }

    private void FollowTarget(GameObject target)
    {
        var pos = target.transform.position;
        pos.y -= 1f;
        transform.position = pos;
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - rigidbody2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rigidbody2d.rotation = angle - 90f;
    }
}
