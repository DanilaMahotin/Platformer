using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Координаты когда лазиет левый: x:-12.04. правый: x:-3.84
    //Координаты Player x 15.8 y 6.011936 z -3.8
    private bool isGrounded;
    private Rigidbody rb;
    private GameManager gameManager;
    private CubeScript[] cubes;
    private ItemScript[] items;
    private Animator anim;
    private bool jumpLeft; 
    private bool jumpRight; 
    private bool upRight; 
    private bool upLeft;
    private Animator cameraAnim;
    private GameObject itemSpawner;
    private GameObject wallRight;
    private GameObject wallLeft;
    private int lastSpeed;

    private void Start()
    {
        transform.localPosition = new Vector3(-7.12f, -2.799575f, 15.33f);
        rb = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        jumpLeft = false;
        jumpRight = false;
        upRight = true;
        upLeft = false;
        cameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        itemSpawner = GameObject.Find("ItemsSpawn");
        wallRight = GameObject.Find("WallRight");
        wallLeft = GameObject.Find("WallLeft");
    }
    private void Update()
    {
        anim.SetBool("jumpLeft", jumpLeft);
        anim.SetBool("JumpRight", jumpRight);
        anim.SetBool("upLeft", upLeft);
        anim.SetBool("upRight", upRight);
        if (upLeft || upRight) 
        {
            isGrounded = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && isGrounded) 
        {
            
            if (upRight)
            {
                SoundManager.Instance.JumpSound();
                Jump(Vector3.left);
                jumpRight = false;
                jumpLeft = true;
                upRight = false;
                cameraAnim.SetBool("jump", true);
            }
            else if(upLeft)
            {
                SoundManager.Instance.JumpSound();
                Jump(Vector3.right);
                jumpLeft = false;
                jumpRight = true;
                upLeft = false;
                cameraAnim.SetBool("jump", true);
            }
        }

        if (transform.localPosition.y <= -14) 
        {
            gameManager.GameOver();
        }
    }

    private void Jump(Vector3 direction) 
    {
        rb.AddForce(direction * 15f, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "WallRight" || other.name == "WallLeft")
        {
            isGrounded = true;
            rb.velocity = Vector3.zero;
            cameraAnim.SetBool("jump", false);
            if (other.name == "WallRight")
            {
                jumpRight = false;
                upRight = true;
                upLeft = false;
                transform.localPosition = new Vector3(-7.12f, -2.799575f, 15.33f);

            } else if (other.name == "WallLeft") 
            {
                jumpLeft = false;
                upRight = false;
                upLeft = true;
                transform.localPosition = new Vector3(-14.9f, -2.799575f, 15.33f);
            }
        }
        if (other.gameObject.CompareTag("coin"))
        {
            PlayerData.Instance.AddMoney(1);
            SoundManager.Instance.CoinSound();
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("coinGold"))
        {
            PlayerData.Instance.AddMoney(25, 1);
            SoundManager.Instance.CoinSound();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("boost"))
        {
            gameManager.PlayerHp++;
            SoundManager.Instance.BoostSound();
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("question")) 
        {
            QuestionScript quest = other.GetComponent<QuestionScript>();
            quest.QuestionItem();
            
        }
        else if (other.gameObject.CompareTag("snowCube"))
        {
            gameManager.PlayerHp--;

        } else if (other.gameObject.CompareTag("barrier"))
        {
            if (GameObject.FindGameObjectWithTag("shield") != null)
            {
                Destroy(GameObject.FindGameObjectWithTag("shield"));
                Destroy(other.gameObject);
            }
            else 
            {
                PlayerFall();
                SoundManager.Instance.FallSound();
            }
        }
        
    }


    public void PlayerFall()
    {
        lastSpeed = gameManager.checkSpeed;
        gameManager.Speed = 0;
        itemSpawner.SetActive(false);
        wallRight.SetActive(false);
        wallLeft.SetActive(false);
        rb.velocity = Vector3.down * 6;
        cameraAnim.SetBool("jump", true);
        if (upLeft || jumpLeft)
        {
            transform.localScale = new Vector3(-3.15f, 3.15f, 3.15f);
            anim.SetBool("respawn", false);
            anim.SetBool("fall", true);
        }
        else 
        {
            anim.SetBool("respawn", false);
            anim.SetBool("fall", true);
        }
        if (GameObject.FindWithTag("shield") != null) 
        {
            Destroy(GameObject.FindWithTag("shield").gameObject);
        }
    }
    public void Respawn() 
    {
        rb.velocity = Vector3.zero;
        transform.localPosition = new Vector3(-7.12f, -2.799575f, 15.33f);
        upRight = true;
        itemSpawner.SetActive(true);
        wallRight.SetActive(true);
        wallLeft.SetActive(true);
        gameManager.Speed = lastSpeed;
        anim.SetBool("fall", false);
        jumpLeft = false;
        jumpRight = false;
        anim.SetBool("respawn", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "WallRight" || other.name == "WallLeft")
        {
            isGrounded = false; 
        }
    }
}
