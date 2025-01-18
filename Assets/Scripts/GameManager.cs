using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    public int Money { get; set; }
    public int PlayerHp { get; set; }
    private bool isGameOver;
    private PlayerMove hero;
    private CubeScript[] cubes;
    private ItemScript[] items;
    public int moneyCheck;
    public int Speed { get; set; }
    public int checkSpeed = -3;
    public int TimeSpeed { get; set; }
    [Header("Интерфейс")]
    public TMP_Text moneyText;
    public TMP_Text healthText;
    public Image clockImg;
    public Image shieldImg;
    public GameObject GameOverPanel;
    public TMP_Text moneyTextGlobal;
    public TMP_Text moneyTextPlus;

    private YandexGame yg;

    private void Start()
    {
        GameOverPanel.SetActive(false);
        isGameOver = false;
        Time.timeScale = 1;
        PlayerHp = 4;
        Speed = -3;
        TimeSpeed = 2;
        clockImg.gameObject.SetActive(false);
        shieldImg.gameObject.SetActive(false);
        yg = GameObject.Find("YandexGame").GetComponent<YandexGame>();
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("shield") != null)
        {
            shieldImg.gameObject.SetActive(true);
        }
        else
        {
            shieldImg.gameObject.SetActive(false);
        }
        if (Time.timeScale == 0.5)
        {
            clockImg.gameObject.SetActive(true);
        }
        else
        {
            clockImg.gameObject.SetActive(false);
        }
        cubes = FindObjectsOfType<CubeScript>();
        items = FindObjectsOfType<ItemScript>();
        foreach (CubeScript cube in cubes)
        {
            cube.SpeedFall = Speed;
        }
        foreach (ItemScript item in items)
        {
            item.SpeedFall = Speed;
        }
        if (PlayerHp <= 0)
        {
            hero = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
            hero.PlayerFall();
        }
        //print(PlayerHp);
        moneyText.text = PlayerData.Instance.sessionMoney.ToString();
        if (moneyCheck >= 5)
        {
            Speed--;
            checkSpeed = Speed;
            TimeSpeed--;
            moneyCheck = 0;
        }

        switch (PlayerHp)
        {
            case 4:
                healthText.text = " 0%";
                break;
            case 3:
                healthText.text = " 33%";
                break;
            case 2:
                healthText.text = " 66%";
                break;
            case 1:
                healthText.text = " 100%";
                break;
        }

        moneyTextGlobal.text = PlayerData.Instance.globalMoney.ToString();
        moneyTextPlus.text = PlayerData.Instance.sessionMoney.ToString();
        if (isGameOver)
        {
            GameOverPanel.SetActive(true);
        }
        else 
        {
            GameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        print("Вы проиграли!");
        Time.timeScale = 0;
        isGameOver = true;
    }

    
    public void RestartBut()
    {
        if (PlayerData.Instance.timer >= 62)
        {
            PlayerData.Instance.timer = 0;
            yg._FullscreenShow();
        }
        else 
        {
            PlayerData.Instance.globalMoney += PlayerData.Instance.sessionMoney;
            PlayerData.Instance.SaveMoney();
            PlayerData.Instance.sessionMoney = 0;
            SceneManager.LoadScene("Game");
        }
        
        
    }
    public void MenuButton()
    {
        if (PlayerData.Instance.timer >= 62)
        {
            PlayerData.Instance.timer = 0;
            yg._FullscreenShow();
        }
        else 
        {
            PlayerData.Instance.globalMoney += PlayerData.Instance.sessionMoney;
            PlayerData.Instance.SaveMoney();
            PlayerData.Instance.sessionMoney = 0;
            SceneManager.LoadScene("Menu");
        }
        
    }
    public void ResumeGame() 
    {
        GameObject[] barriers = GameObject.FindGameObjectsWithTag("barrier");
        foreach (GameObject barrier in barriers) 
        {
            Destroy(barrier);
        }
        isGameOver = false;
        hero = GameObject.FindWithTag("player").GetComponent<PlayerMove>();
        hero.Respawn();
        PlayerHp = 4;
        Time.timeScale = 1;
        print("resume game сработал");
    }
    public void FixRewarded() 
    {
        PlayerData.Instance.ExampleOpenRewardAd(2);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        
        if (!hasFocus)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
