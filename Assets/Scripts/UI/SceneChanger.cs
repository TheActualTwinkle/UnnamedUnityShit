using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Linq;

public class SceneChanger : MonoBehaviour
{
    private static bool isLoading;
    public static bool IsLoading { get => isLoading; }

#if UNITY_EDITOR
    [SerializeField] private List<SceneAsset> nonPlayerScenes;
    public List<SceneAsset> NonPlayerScenes => nonPlayerScenes;
#endif
    private static Animator animator;

    private static Vector3 positionToLoad;
    private static string sceneToLoad;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static void FadeToLevel(string scene)
    {
        animator.SetTrigger("FadeOut");

        sceneToLoad = scene;
        positionToLoad = Vector3.zero;
    }

    public static void FadeToLevel(string scene, Vector3 position)
    {
        FadeToLevel(scene);
        positionToLoad = position;
    }

    public static void ReloadScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        FadeToLevel(activeScene.name, RespawnPoint.RespawnPosition);
    }

    // Animator.
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.transform.position = positionToLoad;

        positionToLoad = Vector3.zero;
    }

    // Animator
    private void EnableInteract()
    {
        isLoading = false;
    }

    // Animator
    private void DisableInteract()
    {
        isLoading = true;
    }

}
