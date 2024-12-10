using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Characteristics
    public int speed = 1;
    public bool movable = true;
    private Rigidbody2D rb;
    public float dirForgiveness = 0.2f;
    
    //Visual Components
    private SpriteRenderer sprite;
    private Animator animator;

    //Trajectory Visualization
    public GameObject trackerPrefab;
    public GameObject[] trackers;
    public int numberOfTrackers = 9;

    //Changing variables
    public int bounces = 0;
    public bool sticky = false;
    public bool gameStart = false;
    private bool rightFacing = true;
    public float horiChangePower = 1.5f;
    public float vertChangePower = 1;
    float vertP = 5;
    float power = 3;

    //forgiveness
    private bool inputForgiveness = false;
    private bool dirFor = false;
    private bool jumpForgiveness = false;

    private bool charging = false;
    public bool flying = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        trackers = new GameObject[numberOfTrackers];

        for (int i = 0; i < numberOfTrackers; i++)
        {
            trackers[i] = Instantiate(trackerPrefab,transform.position, Quaternion.identity);
            trackers[i].SetActive(false);
        }
    }

    void Update()
    {
        if (gameStart)
        {
            float hori = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");
            if (!charging && !flying && movable)
            {
                if (hori != 0 || vert != 0)
                {
                    animator.SetBool("Walking", true);

                }
                else
                {
                    animator.SetBool("Walking", false);

                }
            }
            else
            {
                animator.SetBool("Walking", false);

            }

            //charge while facing right
            if (charging && rightFacing)
            {
                vertP += vert * horiChangePower * Time.deltaTime;
                power += hori * vertChangePower * Time.deltaTime;
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
                else if (power < 0.3f)
                {
                    power = -0.3f;
                    rightFacing = false;
                    sprite.flipX = true;

                }
                animator.SetFloat("overallCharge", vertP + power - 8);
                if (dirFor && hori < 0)
                {
                    power = -3f;
                    rightFacing = false;
                    sprite.flipX = true;
                }
                

            }
            //charge while facing left
            else if (charging && !rightFacing)
            {
                vertP += vert * vertChangePower * Time.deltaTime;
                power += hori * horiChangePower * Time.deltaTime;
                
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
                else if (power > -0.3f)
                {
                    power = 0.3f;
                    rightFacing = true;
                    sprite.flipX = false;
                }
                animator.SetFloat("overallCharge", vertP + -power - 8);
                if (dirFor && hori > 0)
                {
                    power = 3f;
                    rightFacing = true;
                    sprite.flipX = false;
                }

            }
            if (charging)
            {
                for (int i = 0; i <trackers.LongLength; i++)
                {
                    trackers[i].transform.position = PointPos(i * 0.1f);
                }
            }
            //ground movement
            if (movable)
            {
                
                if (!inputForgiveness)
                {
                    if (sticky)
                    {
                        rb.velocity = new Vector2(hori * speed * 0.8f, rb.velocity.y);

                    }
                    else
                    {
                        rb.velocity = new Vector2(hori * speed, rb.velocity.y);

                    }
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
                else if (hori == 0)
                {
                    inputForgiveness = false;
                }
            }
       
            
        }
        
            //begin charging

        if (Input.GetKey(KeyCode.Space) && !charging && !flying && !sticky && !jumpForgiveness)
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0.1f;
            charging = true;

            movable = false;
            if (!rightFacing)
            {
                power *= -1f;
            }
            for(int i = 0; i < trackers.Length; i++)
            {
                trackers[i].SetActive(true);
            }
            //dirFor = true;
            Invoke(nameof(dirForgive), dirForgiveness);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && charging && !flying)
        {
            animator.SetFloat("overallCharge", 0);

            movable = true;
            charging = false;

            rb.gravityScale = 1f;
            vertP = 5;
            power = 3;
            inputForgiveness = true;
            jumpForgiveness = true;
            for (int i = 0; i < trackers.Length; i++)
            {
                trackers[i].SetActive(false);
            }
        }
        //end jump forgiveness
        if (Input.GetKeyUp(KeyCode.Space) && jumpForgiveness)
        {
            jumpForgiveness = false;
        }
        //release the jump
        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            animator.SetBool("Flying", true);

            animator.SetFloat("overallCharge", 0);
            
            charging = false;

            flying = true;
            rb.gravityScale = 1f;
            if (!sticky)
            {
                rb.velocity = new Vector2(power, vertP);

            }
            else
            {
                rb.velocity = new Vector2(power / 2, vertP / 2);

            }
            vertP = 5;
            power = 3;
            transform.Translate(0, 0.1f, 0);
            for (int i = 0; i < trackers.Length; i++)
            {
                trackers[i].SetActive(false);
            }
        }


    }

    public void ResetVariables()
    {
        bounces = 0;
        sticky = false;
            movable = true;
            charging = false;
        animator.SetFloat("overallCharge", 0);

        rb.gravityScale = 1f;
            vertP = 5;
            power = 3;
            inputForgiveness = false;
        flying = false;
        charging = false;
        for (int i = 0; i < trackers.Length; i++)
        {
            trackers[i].SetActive(false);
        }
        animator.SetBool("Flying", false);
        animator.SetBool("Crash", false);

    }

    Vector2 PointPos(float t)
    {
        Vector2 currentPointPos;
        if (!sticky)
        {
            currentPointPos = new Vector2(transform.position.x, transform.position.y + 1) + new Vector2(power, vertP) * t + 0.5f * Physics2D.gravity * (t * t);

        }
        else
        {
            currentPointPos = new Vector2(transform.position.x, transform.position.y + 1) + new Vector2(power / 2, vertP / 2) * t + 0.5f * Physics2D.gravity * (t * t);

        }
        return currentPointPos;
    }

    private void dirForgive()
    {
        dirFor = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("Velocity.y: " + rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) > 2.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                animator.SetBool("Flying", true);

                animator.SetFloat("overallCharge", 0);

                charging = false;

                flying = true;
                rb.gravityScale = 1f;
                
                vertP = 5;
                power = 3;
                transform.Translate(0, 0.1f, 0);
                for (int i = 0; i < trackers.Length; i++)
                {
                    trackers[i].SetActive(false);
                }
                Debug.Log("Velcoity.x: " + rb.velocity.x);
                if (Mathf.Abs(rb.velocity.x) < 1.2f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * Mathf.Pow(0.5f, bounces));
                    bounces++;

                }

                flying = true;
                movable = false;
            }
            else
            {
                Debug.Log("Not enough velocity");
                ResetVariables();
            }
        }
    }
}
