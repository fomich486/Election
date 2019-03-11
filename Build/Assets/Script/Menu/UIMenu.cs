using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    private string sceneFolder = "Scenes/";
    public void LoadGame()
    {
        //add selected head info to settings
        SceneManager.LoadScene(sceneFolder + "Game");
    }
}
