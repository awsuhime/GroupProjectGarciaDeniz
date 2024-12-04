using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class groundDetector : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public ParticleSystem dirtParticle;
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        
        if (playerMovement.flying)
        {
            if (collision.CompareTag("Terrain"))
            {
                Debug.Log("Groundhit; name:" + collision.gameObject.name);
                dirtParticle.Play();
                playerMovement.flying = false;
                playerMovement.movable = true;
                animator.SetBool("Flying", false);
                animator.SetBool("Crash", false);
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Glue"))
        {
            playerMovement.sticky = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Glue"))
        {
            playerMovement.sticky = false;
        }
    }
}
