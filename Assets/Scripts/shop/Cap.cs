using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCap", menuName = "Shop/Cap")]
public class Cap : ScriptableObject
{
    public GameObject Prefab;
    public bool isBought;
    public bool isUsed;
    
}
