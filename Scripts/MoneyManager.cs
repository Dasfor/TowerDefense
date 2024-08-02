using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public Text moneyText;
    public int GameMoney = 100;
    public int TotalMoney;


    public void Awake()
    {
        Instance = this;
        TotalMoney = PlayerPrefs.GetInt("coins");
    }


    public void Update()
    {
        moneyText.text = GameMoney.ToString();
    }
    public void LevelEnd()
    {
        PlayerPrefs.SetInt("coins", (int)TotalMoney + (int)GameMoney);
        SceneManager.LoadScene(0);
    }
}
