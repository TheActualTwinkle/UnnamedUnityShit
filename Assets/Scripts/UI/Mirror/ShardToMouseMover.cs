using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MirrorShard), typeof(Collider2D))]
public class ShardToMouseMover : MonoBehaviour 
{
    public static readonly float distanceToAutoBringing = 5f;

    private bool CanMove { get => !shard.IsActivated; }

    private Vector3 mouseStartPosition;
    private Vector3 targetStartPosition;

    private Mirror mirror;
    private MirrorShard shard;

    private void Start()
    {
        mirror = GetComponentInParent<Mirror>();
        shard = GetComponent<MirrorShard>();
    }

    private void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanMove)
        {
            mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetStartPosition = transform.position;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && CanMove)
        {
            if (Vector3.Distance(Vector3.zero, transform.localPosition) <= distanceToAutoBringing)
            {
                transform.localPosition = Vector3.zero;

                shard.Activate();
                SaveLoadSystem.SaveMirrorData(mirror);
            }
        }
    }

    private void OnMouseDrag()
    {
        if (CanMove)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 relativeMousePosition = mousePosition - mouseStartPosition;
            transform.position = targetStartPosition + relativeMousePosition;
        }
    }
}
