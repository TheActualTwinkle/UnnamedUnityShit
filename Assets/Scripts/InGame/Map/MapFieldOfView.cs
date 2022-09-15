using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MapFieldOfView : MonoBehaviour
{
    public event Action<DrawnMapElement> MapEelementEnterEvent;
    public event Action<DrawnMapElement> MapEelementExitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DrawnMapElement element))
        {
            MapEelementEnterEvent?.Invoke(element);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DrawnMapElement element))
        {
            MapEelementExitEvent?.Invoke(element);
        }
    }
}
