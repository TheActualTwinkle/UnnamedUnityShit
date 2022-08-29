using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SceneTransfer : MonoBehaviour
{
    [SerializeField] private string sceneToTransfer;

    [SerializeField] private Vector3 positionToTransfer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController p) && InteractAccessor.CanInteract == true)
        {
            SceneChanger.FadeToLevel(sceneToTransfer, positionToTransfer);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
