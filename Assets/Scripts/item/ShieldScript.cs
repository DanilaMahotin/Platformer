using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindWithTag("player");
    }
    private void Update()
    {
        Vector3 playerPos = Player.transform.position;
        playerPos.y = -0.45f;
        transform.position = playerPos;
    }
}
