using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliveryQuest", menuName = "QuestObject/DeliveryQuest")]
public class DeliveryQuest : QuestObject
{
    [Header("DeliveryQuest-------------------------------------------------------------------------")]

    [SerializeField] private GameObject whatToDeliver;
    public GameObject WhatToDeliver { get => whatToDeliver; }

    [SerializeField] private Vector3 whereToDeliver;
    public Vector3 WhereToDeliver { get => whereToDeliver; }
}
