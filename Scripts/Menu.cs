using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text coinsText;
    public int TotalMoney;

    void Start()
    {
        TotalMoney = PlayerPrefs.GetInt("coins");
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = TotalMoney.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
