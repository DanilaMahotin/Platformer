using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Shop/Skin")]
public class Skin : ScriptableObject
{
    public GameObject Prefab;
    public bool isBought;
    public bool isUsed;
}
