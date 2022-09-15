using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DrawnMapElement : MonoBehaviour
{
    [SerializeField] private bool drawAnyway;
    public bool DrawAnyway => drawAnyway;
}
