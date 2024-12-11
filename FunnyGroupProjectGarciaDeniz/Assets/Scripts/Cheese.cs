using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cheese : MonoBehaviour
{
    public GameObject winUI;
    public TextMeshProUGUI deathText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            PlayerMovement.gameStart = false;
            winUI.SetActive(true);
            deathText.text = "DEATHS: " + PlayerHealth.deaths;
        }
    }
}
