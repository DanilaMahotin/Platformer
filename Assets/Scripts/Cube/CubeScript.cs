using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeScript : MonoBehaviour
{
    public int SpeedFall { get; set; }
 
    public  void CubeFall()
    {
        transform.Translate(new Vector3(0, SpeedFall * Time.deltaTime, 0));
    }
    

}
