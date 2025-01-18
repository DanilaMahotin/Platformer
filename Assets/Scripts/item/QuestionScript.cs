using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScript : ItemScript
{
    public GameObject shieldPrefab;
    private bool timeAbl;
    private void Start()
    {
    }

    void Update()
    {
        ItemFall();
    }

    public void QuestionItem() 
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            StartCoroutine(TimeAbility());
        }
        else 
        {
            if (GameObject.FindGameObjectWithTag("shield") != null)
            {
                StartCoroutine(TimeAbility());
            }
            else 
            {
                DefAbility();
            }
        }
    }

    private IEnumerator TimeAbility() 
    {
        timeAbl = true;
        Time.timeScale = 0.5f;
        Renderer render = GetComponent<Renderer>();
        Collider col = GetComponent<Collider>();
        render.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(4f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
    public void DefAbility() 
    {
        timeAbl = false;
        Instantiate(shieldPrefab);
        Destroy(gameObject);
    }
}
