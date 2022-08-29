using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorShard : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private bool isActivated;
    public bool IsActivated { get => isActivated; }

    public void Activate()
    {
        isActivated = true;
    }
}
