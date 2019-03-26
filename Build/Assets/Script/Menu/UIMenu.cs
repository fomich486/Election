using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIMenu : MonoBehaviour
{

    public UnityEvent loadEvent;
    private string sceneFolder = "Scenes/";
    public void LoadGame()
    {
        //add selected head info to settings
        SceneManager.LoadScene(sceneFolder + "Game");
    }

    private void Start()
    {
        if (Settings.Instance.headSelection)
        {
            loadEvent.Invoke();
        }
        Settings.Instance.headSelection = false;
    }
}
