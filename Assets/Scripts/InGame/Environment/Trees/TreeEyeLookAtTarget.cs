using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEyeLookAtTarget : MonoBehaviour
{
    private Vector2 lookDirection;

    [SerializeField] private GameObject target;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            lookDirection = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y).normalized;

            animator.SetFloat("LookX", lookDirection.x);
            animator.SetFloat("LookY", lookDirection.y);
        }
    }
}
