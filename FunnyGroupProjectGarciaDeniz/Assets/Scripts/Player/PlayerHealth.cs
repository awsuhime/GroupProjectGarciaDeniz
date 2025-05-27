using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int deaths = 0;
    private Checkpoint checkpointScript;
    public GameObject deathParticle;
    private int priority;
    public  GameObject currentCheckpoint;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
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
        deaths++;
        Instantiate(deathParticle, new (transform.position.x, transform.position.y + 1.7f, -1), Quaternion.Euler(0,-90,90));
        playerMovement.ResetVariables();
        gameObject.transform.position = currentCheckpoint.transform.position;
        rb.velocity = Vector2.zero;
    }


}
