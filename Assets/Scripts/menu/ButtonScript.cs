using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ButtonScript : MonoBehaviour
{
    public int Money{ get; set; }
    public TMP_Text moneyText;
    private GameObject yg;
    void Start()
    {
        UpdateMoneyText();
        yg = GameObject.Find("YandexGame");
    }

    
    

    public void PlayButton() 
    {
        DontDestroyOnLoad(yg);
        SceneManager.LoadScene("Game");
    }
    public void ShopButton() 
    {
        DontDestroyOnLoad(yg);
        SceneManager.LoadScene("Shop");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateMoneyText() 
    {
            int globalMoney = PlayerData.Instance.GetGlobalMoney();
            moneyText.text = globalMoney.ToString();
    }
    public void FixRewarded() 
    {
        PlayerData.Instance.ExampleOpenRewardAd(1);
    }
}
