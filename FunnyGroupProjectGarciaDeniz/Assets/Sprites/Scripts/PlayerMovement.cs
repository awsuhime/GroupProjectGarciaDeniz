using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 1;
    public bool movable = true;
    private Rigidbody2D rb;
    public GameObject tracker;
    private Rigidbody2D trackerRb;
    private SpriteRenderer sprite;

    private bool rightFacing = true;
    public float changePower = 1;
    float vertP = 5;
    float power = 3;

    private bool trackerCD = false;
    float trackerStart;
    public float trackerInterval = 0.5f;

    private bool charging = false;
    public bool flying = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        
        //charge while facing right
        if (charging && rightFacing)
        {
            vertP += vert * changePower * Time.deltaTime; 
            power += hori * changePower * Time.deltaTime;
            //Caveman clamp
            if (vertP > 7f)
            {
                vertP = 7f;
            }
            else if (vertP < 3f)
            {
                vertP = 3f;
            }
            if (power > 5f)
            {
                power = 5f;
            }
            else if (power < 1f)
            {
                power = 1f;
            }

            if (!trackerCD)
            {
                GameObject target = Instantiate(tracker, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                trackerRb = target.GetComponent<Rigidbody2D>();
                trackerRb.velocity = new Vector2(power, vertP);
                trackerCD = true;
                trackerStart = Time.time;
              
            }
            else
            {
                float trackerLeft = Time.time - trackerStart;
                if (trackerLeft > trackerInterval)
                {
                    trackerCD = false;
                }
            }

        }
        //charge while facing left
        else if (charging && !rightFacing)
        {
            vertP += vert * changePower * Time.deltaTime;
            power += hori * changePower * Time.deltaTime;
            //Caveman clamp
            if (vertP > 7f)
            {
                vertP = 7f;
            }
            else if (vertP < 3f)
            {
                vertP = 3f;
            }
            if (power < -5f)
            {
                power = -5f;
            }
            else if (power > -1f)
            {
                power = -1f;
            }

            if (!trackerCD)
            {
                GameObject target = Instantiate(tracker, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                trackerRb = target.GetComponent<Rigidbody2D>();
                trackerRb.velocity = new Vector2(power, vertP);
                trackerCD = true;
                trackerStart = Time.time;

            }
            else
            {
                float trackerLeft = Time.time - trackerStart;
                if (trackerLeft > trackerInterval)
                {
                    trackerCD = false;
                }
            }

        }
        //ground movement
        if (movable)
        {            
            rb.velocity = new Vector2(hori * speed,rb.velocity.y);   
            if (hori < 0)
            {
                sprite.flipX = true;
                rightFacing = false;
            }
            else if (hori > 0)
            {
                sprite.flipX = false;
                rightFacing = true;
            }
            
        }
        //begin charging
        if (Input.GetKeyDown(KeyCode.Space) && !charging && !flying)
        {
            rb.velocity = new Vector2(0, rb.velocity.y / 4);
            rb.gravityScale = 0.1f;
            charging = true;
            movable = false;
            if (!rightFacing)
            {
                power *= -1f;
            }
        }
        //release the jump
        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            charging = false;
            flying = true;
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(power, vertP);
            trackerCD = false;
            vertP = 5;
            power = 3;
        }
        

    }
    


    
}
