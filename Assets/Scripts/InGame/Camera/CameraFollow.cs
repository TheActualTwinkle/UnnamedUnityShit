using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    [SerializeField] private SceneChanger sceneChanger;

    [SerializeField] private float cameraSpeed;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float botLimit;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (sceneChanger == null)
        {
            sceneChanger = FindObjectOfType<SceneChanger>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (PlayerController.Instance != null)
        {
            FollowTarget(PlayerController.Instance.transform);
        }
    }

    private void FollowTarget(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z - 10f);

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, -leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, -botLimit, topLimit),
            transform.position.z
        );
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(-leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(-leftLimit, -botLimit), new Vector2(rightLimit, -botLimit));
        Gizmos.DrawLine(new Vector2(-leftLimit, topLimit), new Vector2(-leftLimit, -botLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, -botLimit));
    }
}
