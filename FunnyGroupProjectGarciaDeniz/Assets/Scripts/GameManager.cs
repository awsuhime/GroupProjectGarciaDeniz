using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    private AudioSource audioSource;
    public AudioClip easyMusic;
    public AudioClip hardMusic;
    public AudioClip mtMusic;
    public GameObject startUI;
    public GameObject hardcoreCheckpoint;
    public GameObject mouseTrapCheckpoint;
    public GameObject easyBG;
    public GameObject hardBG;
    public GameObject pauseUI;
    public bool paused = false;
    
    void Start()
    {
        Time.timeScale = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.gameStart && !paused)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PlayerMovement.gameStart && paused)
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            paused = false;
        }
    }

    public void returnToTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gameStart()
    {
        PlayerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
        audioSource.clip = easyMusic;
        audioSource.Play();
    }

    public void HardcoreStart()
    {
        PlayerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
        playerHealth.currentCheckpoint = hardcoreCheckpoint;
        playerMovement.gameObject.transform.position = hardcoreCheckpoint.transform.position;
        easyBG.SetActive(false);
        hardBG.SetActive(true);
        audioSource.clip = hardMusic;
        audioSource.Play();
    }

    public void MouseTrapStart()
    {
        PlayerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
        playerHealth.currentCheckpoint = mouseTrapCheckpoint;
        playerMovement.gameObject.transform.position = mouseTrapCheckpoint.transform.position;
        easyBG.SetActive(false);
        hardBG.SetActive(true);
        audioSource.clip = mtMusic;
        audioSource.Play();
    }
}
