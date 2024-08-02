using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button menuButton, musicButton, restartButton, exitButton;
    public bool act = false;

    void Start()
    {

    }

    // Update is called once per frame
    public void Menu()
    {
        if (act == false)
        {
            exitButton.interactable = true;
            musicButton.interactable = true;
            act = true;
            return;
        }
        if (act == true)
        {
            exitButton.interactable = false;
            musicButton.interactable = false;
            act = false;
            return;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}