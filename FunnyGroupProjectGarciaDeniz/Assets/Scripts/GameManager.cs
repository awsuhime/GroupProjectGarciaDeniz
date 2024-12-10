using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    private AudioSource audioSource;
    public AudioClip easyMusic;
    public AudioClip hardMusic;
    public GameObject startUI;
    public GameObject hardcoreCheckpoint;
    public GameObject easyBG;
    public GameObject hardBG;
    
    void Start()
    {
        Time.timeScale = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void gameStart()
    {
        playerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
        audioSource.clip = easyMusic;
        audioSource.Play();
    }

    public void HardcoreStart()
    {
        playerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
        playerHealth.currentCheckpoint = hardcoreCheckpoint;
        playerMovement.gameObject.transform.position = hardcoreCheckpoint.transform.position;
        easyBG.SetActive(false);
        hardBG.SetActive(true);
        audioSource.clip = hardMusic;
        audioSource.Play();
    }
}
