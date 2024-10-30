using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Checkpoint checkpointScript;
    private int priority;
    public GameObject currentCheckpoint;
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y <= -5)
        {
            Respawn();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            checkpointScript = collision.GetComponent<Checkpoint>();
            if (checkpointScript.priority > priority)
            {
                
                currentCheckpoint = collision.gameObject;
                priority = checkpointScript.priority;

            }

        }
        else if (collision.CompareTag("Spike"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        gameObject.transform.position = currentCheckpoint.transform.position;
    }


}
