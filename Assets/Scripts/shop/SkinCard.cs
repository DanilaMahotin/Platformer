using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinCard : MonoBehaviour
{
    public Skin skin;
    public Button buyButton;
    private ButtonScript UImanager;

    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UImanager = GameObject.Find("UIManager").GetComponent<ButtonScript>();
        CheckSkin();
    }

    private void Update()
    {
        UpdateButton();
    }

    private void CheckSkin() 
    {
        int skinIndex = PlayerData.Instance.allSkins.IndexOf(skin);
        if (PlayerData.Instance.boughtSkins[skinIndex])
        {
            skin.isBought = true;
        }
        else
        {
            skin.isBought = false;
        }
    }
    private void UpdateButton()
    {
        if (PlayerData.Instance.selectedSkin != skin && skin.isBought)
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "Использовать";
            buyButton.interactable = true;
        }
        else if (PlayerData.Instance.selectedSkin == skin)
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "Использовать";
            buyButton.interactable = false;
        }
        else if (!skin.isBought)
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "500 монет";
        }
    }

    private void OnBuyButtonClicked()
    {
        int test = PlayerData.Instance.allSkins.IndexOf(skin);
        print(PlayerData.Instance.boughtSkins[test]);
        if (skin.isBought)
        {
            
                skin.isUsed = true;
                if (PlayerData.Instance.selectedSkin != null)
                {
                    Skin skinUsed = PlayerData.Instance.selectedSkin;
                    skinUsed.isUsed = false;
                }
                PlayerData.Instance.selectedSkin = skin;
                PlayerData.Instance.SaveData();
            
        }
        else
        {

            int skinIndex = PlayerData.Instance.allSkins.IndexOf(skin);
            print(skinIndex);
            if (skinIndex >= 0)
            {
                PlayerData.Instance.BuySkin(skinIndex, skin);
            }
        }
        UpdateButton(); // Обновляем состояние кнопки
    }
}
