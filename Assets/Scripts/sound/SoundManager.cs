using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource backAudio;
    private AudioSource JumpSource;
    private AudioSource ItemSource;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip boostClip;
    public AudioClip fallClip;
    public AudioClip buyClip;
    private int timerSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        backAudio = gameObject.GetComponent<AudioSource>();
        JumpSource = gameObject.AddComponent<AudioSource>();
        JumpSource.volume = 0.5f;
        ItemSource = gameObject.AddComponent<AudioSource>();
        ItemSource.volume = 0.5f;
        timerSound = 0;
    }

    public void JumpSound() 
    {
        JumpSource.clip = jumpClip;
        JumpSource.Play();
    }
    public void CoinSound() 
    {
        ItemSource.clip = coinClip;
        ItemSource.Play();
    }
    public void BoostSound() 
    {
        ItemSource.clip = boostClip;
        ItemSource.Play();
    }
    public void FallSound() 
    {
        ItemSource.clip = fallClip;
        ItemSource.Play();
    }
    
    public void BuySound() 
    {
        ItemSource.clip = buyClip;
        ItemSource.Play();
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            backAudio.Pause();
            JumpSource.volume = 0;
            ItemSource.volume = 0;
        }
        else
        {
            backAudio.UnPause();
            JumpSource.volume = 0.5f;
            ItemSource.volume = 0.5f; 
        }
    }

}
