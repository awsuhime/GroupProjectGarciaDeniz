using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DugShoot : MonoBehaviour
{
    public GameObject Player;
    public GameObject projectile;
    private Animator animator;
    public int sightDistance = 40;
    public float shootSpeed = 1f;
    public bool shootCD = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < sightDistance)
        {
            if (!shootCD)
            {
                Instantiate(projectile, transform.position, transform.rotation);
                shootCD = true;
                Invoke(nameof(Reload), shootSpeed);
                animator.SetBool("Shoot", true);
            }
        }
    }

    void Reload()
    {
        shootCD = false;
    }

    void EndShoot()
    {
        animator.SetBool("Shoot", false);
    }
    


}
