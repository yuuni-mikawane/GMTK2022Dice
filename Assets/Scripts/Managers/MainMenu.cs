using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialObj;
    public GameObject creditObj;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Tutorial()
    {
        tutorialObj.SetActive(true);
    }

    public void Credit()
    {
        creditObj.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
