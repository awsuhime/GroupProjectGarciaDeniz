using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject startUI;
    
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }

    public void gameStart()
    {
        playerMovement.gameStart = true;
        startUI.SetActive(false);
        Time.timeScale = 1;
    }
}
