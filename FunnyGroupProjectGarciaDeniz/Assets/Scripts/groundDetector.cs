using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetector : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public ParticleSystem dirtParticle;
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
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
            }
           
        }
    }
}
