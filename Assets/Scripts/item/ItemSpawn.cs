using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject coinPrefabDefault;
    public GameObject coinPrefabGold;
    public GameObject boostPrefab;
    public GameObject QuestionPrefab;
    private GameManager manager;
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        Spawner();
    }
    private void SpawnItem()
    {
        int rand = Random.Range(0, 100); // Генерируем случайное число от 0 до 99
        GameObject itemToSpawn;
        if (rand < 70)
        {
            itemToSpawn = coinPrefabDefault;
        }
        else if (rand < 80)
        {
            itemToSpawn = coinPrefabGold;
        }
        else if (rand < 90) 
        {
            if (manager.PlayerHp < 4)
            {
                itemToSpawn = boostPrefab;
            }
            else 
            {
                itemToSpawn = coinPrefabDefault;
            }
        }
        else 
        { 
            if (Time.timeScale != 0.5f)
            {
                itemToSpawn = QuestionPrefab;
            }
            else
            {
                rand = Random.Range(0, 100);
                if (rand < 20) 
                {
                    itemToSpawn = boostPrefab;
                }
                else 
                {
                    itemToSpawn = coinPrefabDefault;
                }
            }
        }
        Vector3 spawnPos = transform.position;
        spawnPos.y = 21f;
        Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
    }

    void Spawner() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 14f))
        {
            if (hit.collider != null)
            {
            }
        }
        else
        {
            SpawnItem();
        }
    }
}
