using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private Skin skin;

    void Start()
    {
        GameObject nowSkin = GameObject.FindWithTag("player");
        skin = PlayerData.Instance.selectedSkin;

        if (nowSkin !=null) 
        {
            Destroy(nowSkin);
            Instantiate(skin.Prefab, gameObject.transform);
        }
        
    }
   
}
