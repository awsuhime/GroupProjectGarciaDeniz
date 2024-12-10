using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class groundDetector : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject dirtParticle;
    private Animator animator;
    public GameObject stickyOverlay;
    

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
                playerMovement.flying = false;
                playerMovement.movable = true;
                playerMovement.bounces = 0;
                Instantiate(dirtParticle, new(transform.position.x, transform.position.y + 0.6f, -1), Quaternion.Euler(0, -90, 90));

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
            stickyOverlay.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Glue"))
        {
            playerMovement.sticky = false;
            stickyOverlay.SetActive(false);
        }
    }
}
