using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }
    private GameManager manager;
    private ButtonScript uiManager;
    public int globalMoney;
    public int sessionMoney;
    public Cap selectedCap;
    public Skin selectedSkin;
    public float timer;

    // Список всех доступных скинов
    public List<Skin> allSkins = new List<Skin>();
    public List<Cap> allCaps = new List<Cap>();

    // Список для хранения информации о купленных скинах
    public List<bool> boughtSkins = new List<bool>();
    public List<bool> boughtCaps = new List<bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadSaveData();
        }
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (SceneManager.GetActiveScene().name == "Game")
        {
            manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (SceneManager.GetActiveScene().name == "Shop" || SceneManager.GetActiveScene().name == "Menu")
        {
            uiManager = GameObject.Find("UIManager").GetComponent<ButtonScript>();
        }
        
    }

    
    public void AddMoney(int amount)
    {
        sessionMoney += amount;
        manager.moneyCheck += amount;
    }

    public void AddMoney(int amount, int amount2)
    {
        sessionMoney += amount;
        manager.moneyCheck += amount2;
    }

    public int GetGlobalMoney()
    {
        return globalMoney;
    }

    public void ResetMoney()
    {
        sessionMoney = 0;
    }

    private void AddMoneyRewarded()
    {
        globalMoney += 200;
        uiManager.UpdateMoneyText();
        SaveData();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadSaveData;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadSaveData;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void Rewarded(int id)
    {
        if (id == 1)
            AddMoneyRewarded();
        else if (id == 2)
            manager.ResumeGame();
    }

    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    public void SaveMoney() 
    {
        YandexGame.savesData.Money = globalMoney;
    }
    public void SaveData()
    {
        YandexGame.savesData.Money = globalMoney;
        YandexGame.savesData.BoughtSkins = boughtSkins;
        YandexGame.savesData.SelectedSkinIndex = allSkins.IndexOf(selectedSkin);
        YandexGame.savesData.BoughtCaps = boughtCaps;
        YandexGame.savesData.SelectedCapIndex = allCaps.IndexOf(selectedCap);
        YandexGame.SaveProgress();
    }

    private void LoadSaveData()
    {
        globalMoney = YandexGame.savesData.Money;

        boughtSkins = YandexGame.savesData.BoughtSkins;
        boughtCaps = YandexGame.savesData.BoughtCaps;
        while (boughtSkins.Count < allSkins.Count)
        {
            boughtSkins.Add(false); 
        }
        while (boughtCaps.Count < allCaps.Count)
        {
            boughtCaps.Add(false); 
        }
        boughtSkins[0] = true;
        boughtCaps[0] = true;


        // Загрузка выбранного скина
        int selectedSkinIndex = YandexGame.savesData.SelectedSkinIndex;
        if (selectedSkinIndex >= 0 && selectedSkinIndex < allSkins.Count)
        {
            selectedSkin = allSkins[selectedSkinIndex];
            selectedSkin.isUsed = true; // Устанавливаем флаг, что скин выбран
        }
        int selectedCapIndex = YandexGame.savesData.SelectedCapIndex;
        if (selectedCapIndex >= 0 && selectedCapIndex < allCaps.Count)
        {
            selectedCap = allCaps[selectedCapIndex];
            selectedCap.isUsed = true; // Устанавливаем флаг, что скин выбран
        }
    }

    public void BuySkin(int skinIndex, Skin skin)
    {
        if (skinIndex < 0 || skinIndex >= allSkins.Count)
        {
            
            return;
        }

        Skin skinToBuy = allSkins[skinIndex];

        if (!boughtSkins[skinIndex]) // Проверяем, куплен ли скин
        {
            if (globalMoney >= 500) // Цена скина
            {
                skin.isBought = true;
                boughtSkins[skinIndex] = true; // Помечаем скин как купленный
                globalMoney -= 500; // Вычитаем деньги
                uiManager.UpdateMoneyText();
                SaveData();
                
            }
            else
            {
                
            }
        }
        else
        {
            
        }
    }
    public void BuyCap(int capIndex, Cap cap)
    {
        print(capIndex);
        if (capIndex < 0 || capIndex >= allCaps.Count)
        {
            
            return;
        }

        Cap capToBuy = allCaps[capIndex];

        if (!boughtCaps[capIndex]) // Проверяем, куплен ли скин
        {
            if (globalMoney >= 250) // Цена скина
            {
                cap.isBought = true;
                boughtCaps[capIndex] = true; // Помечаем скин как купленный
                globalMoney -= 250; // Вычитаем деньги
                uiManager.UpdateMoneyText();
                SaveData();
                
            }
            else
            {
                
            }
        }
        else
        {
            
        }
    }
}
