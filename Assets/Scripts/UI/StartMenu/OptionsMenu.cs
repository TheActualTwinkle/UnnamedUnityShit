using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private static SetVolume[] volumeSliders;
    [SerializeField] private GameObject mainMenu;

    private void Update()
    {
        if (gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void ShowDevelopers()
    {
        Application.OpenURL("https://sun9-12.userapi.com/impg/-6ikE0Qv81U4lr9Oa91YPevTJzGmApwfOYcb5w/Hf1n-NZdG38.jpg?size=992x992&quality=96&sign=c0adf173ce76091db38cd3ce49c0119c&type=album");
    }

    public void Initialize()
    {
        volumeSliders = GetComponentsInChildren<SetVolume>(true);
        foreach (SetVolume slider in volumeSliders) 
        { 
            slider.Init(); 
        }
    }
}
