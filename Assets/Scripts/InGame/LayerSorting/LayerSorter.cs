using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LayerSorter : MonoBehaviour
{
    private Renderer spriteRenderer;

    private int sortingOrderBase;
    [SerializeField] private int offset;

    private void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
        sortingOrderBase = spriteRenderer.sortingOrder;
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (sortingOrderBase - Mathf.RoundToInt(transform.position.y) + offset);
    }
}
