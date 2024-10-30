using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public Rigidbody2D playerRb;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x * -0.5f, playerRb.velocity.y);
        }
    }

}
