using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapCard : MonoBehaviour
{
    public Cap cap;
    public Button buyButton;
    private ButtonScript UImanager;

    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UImanager = GameObject.Find("UIManager").GetComponent<ButtonScript>();
        CheckCap();
    }

    private void Update()
    {
        UpdateButton();
    }

    private void CheckCap()
    {
        int capIndex = PlayerData.Instance.allCaps.IndexOf(cap);
        if (PlayerData.Instance.boughtCaps[capIndex])
        {
            cap.isBought = true;
        }
        else
        {
            cap.isBought = false;
        }
    }
    private void UpdateButton()
    {
        if (PlayerData.Instance.selectedCap != cap && cap.isBought)
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "Использовать";
            buyButton.interactable = true;
        }
        else if (PlayerData.Instance.selectedCap == cap)
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "Использовать";
            buyButton.interactable = false;
        } else if (!cap.isBought) 
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "250 монет";
        }
    }

    private void OnBuyButtonClicked()
    {
        if (cap.isBought)
        {
            
                cap.isUsed = true;
                if (PlayerData.Instance.selectedCap != null)
                {
                    Cap capUsed = PlayerData.Instance.selectedCap;
                    capUsed.isUsed = false;
                }
                PlayerData.Instance.selectedCap = cap;
                PlayerData.Instance.SaveData();
            
        }
        else
        {
            int capIndex = PlayerData.Instance.allCaps.IndexOf(cap);
            if (capIndex >= 0)
            {
                PlayerData.Instance.BuyCap(capIndex, cap);
            }
        }
        UpdateButton(); // Обновляем состояние кнопки
    }
}
