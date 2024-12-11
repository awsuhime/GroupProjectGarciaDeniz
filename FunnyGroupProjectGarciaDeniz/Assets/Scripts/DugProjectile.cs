using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DugProjectile : MonoBehaviour
{
    public float speed = 3;
    public bool leftFacing;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (leftFacing)
        {
            transform.Translate(-0.1f * speed, 0, 0);

        }
        else
        {
            transform.Translate(0.1f * speed, 0, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }


}
