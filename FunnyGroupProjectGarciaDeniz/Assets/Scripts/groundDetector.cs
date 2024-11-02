using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDetector : MonoBehaviour
{
    private PlayerMovement playerMovement;
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
                playerMovement.flying = false;
                playerMovement.movable = true;
            }
           
        }
    }
}
