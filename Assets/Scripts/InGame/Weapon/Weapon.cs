using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayerSorter))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private AudioClip[] attackSound;
    public AudioClip[] AttackSound
    {
        get { return attackSound; }
    }

    [SerializeField] private float damage;
    public float Damage
    {
        get { return damage; }
    }

    [SerializeField] private string weaponName;
    public string WeaponName
    {
        get { return weaponName; }
    }

    [TextArea (2, 4)]
    [SerializeField] private string description;
    public string Description
    {
        get { return description; }
    }

    // public abstract void Attack();
}
