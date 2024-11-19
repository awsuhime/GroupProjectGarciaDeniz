using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Characteristics
    public int speed = 1;
    public bool movable = true;
    private Rigidbody2D rb;
    
    //Visual Components
    private SpriteRenderer sprite;
    private Animator animator;

    //Trajectory Visualization
    public GameObject trackerPrefab;
    public GameObject[] trackers;
    public int numberOfTrackers = 9;

    //Changing variables
    public bool gameStart = false;
    private bool rightFacing = true;
    public float changePower = 1;
    float vertP = 5;
    float power = 3;
    private bool inputForgiveness = false;


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
            if (!movable)
            {
                animator.SetBool("Walking", false);

            }

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
                animator.SetFloat("overallCharge", vertP + power - 8);

                

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
                animator.SetFloat("overallCharge", vertP + -power - 8);


               

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
                if (hori != 0)
                {
                    animator.SetBool("Walking", true);
                }
                else
                {
                    animator.SetBool("Walking", false);

                }
                if (!inputForgiveness)
                {
                    rb.velocity = new Vector2(hori * speed, rb.velocity.y);
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
            for(int i = 0; i < trackers.Length; i++)
            {
                trackers[i].SetActive(true);
            }
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
            for (int i = 0; i < trackers.Length; i++)
            {
                trackers[i].SetActive(false);
            }
        }
        //release the jump
        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            animator.SetBool("Flying", true);

            animator.SetFloat("overallCharge", 0);
            
            charging = false;
            flying = true;
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(power, vertP);
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
            movable = true;
            charging = false;
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
        Vector2 currentPointPos = new Vector2 (transform.position.x, transform.position.y + 1) + new Vector2(power, vertP) * t + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }


}
