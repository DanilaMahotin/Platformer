using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    public int SpeedFall { get; set; }
    
    public void ItemFall()
    {
        transform.Translate(new Vector3(0, SpeedFall * Time.deltaTime, 0));
    }
}
