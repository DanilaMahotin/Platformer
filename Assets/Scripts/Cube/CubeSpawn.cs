using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public GameObject IceCube;
    public GameObject SnowCube;
    public GameObject Barrier;
    private GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Spawner();
    }

    public void SpawnCube()
    {
        int rand = Random.Range(0, 100);
        GameObject cubeToSpawn;
        if (rand < 80) 
        {
            cubeToSpawn = IceCube;
        }
        else if (rand < 90)
        {
            bool goSpawn; 
            GameObject[] otherBarriers = GameObject.FindGameObjectsWithTag("barrier");
            if (otherBarriers.Length > 0)
            {
                goSpawn = false;
                for (int i = 0; i < otherBarriers.Length; i++)
                {
                    if (otherBarriers[i].transform.position.y < 16)
                    {
                        goSpawn = true;
                    }
                }
            }
            else 
            {
                goSpawn = true;
            }

            if (goSpawn)
            {
                cubeToSpawn = IceCube;
                float[] axisX = { 9.18f, 0.69f };
                int randomIndex = Random.Range(0, axisX.Length);
                float X = axisX[randomIndex];
                Vector3 spawner = transform.position;
                spawner.x = X;
                float barFixY = -(float)manager.Speed / 100;
                float barSpawnY = 21 - barFixY;
                spawner.y = barSpawnY;
                Instantiate(Barrier, spawner, Quaternion.identity);
                
            }
            else 
            {
                
                cubeToSpawn = IceCube;
            } 
        }
        else 
        {
            cubeToSpawn = SnowCube;
        }

        Vector3 spawnPos = transform.position;
        float fixY = -(float)manager.Speed / 100;
        float spawnYpos = 21 - fixY;
        spawnPos.y = spawnYpos;
        Instantiate(cubeToSpawn, spawnPos, Quaternion.identity);
    }

    private void Spawner()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 9f))
        {
            if (hit.collider != null)
            {
            }
        }
        else 
        {
            SpawnCube();
        }
    }
}


