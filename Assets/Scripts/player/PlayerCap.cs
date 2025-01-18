using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCap : MonoBehaviour
{
    private Cap cap;

    void Start()
    {
        GameObject nowCap = GameObject.FindWithTag("headwear");
        cap = PlayerData.Instance.selectedCap;

        if (nowCap != null)
        {
            Destroy(nowCap);
            Instantiate(cap.Prefab, gameObject.transform);
            
        }

    }
}
