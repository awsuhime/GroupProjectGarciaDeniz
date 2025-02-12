using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetrap : MonoBehaviour
{
    public float verticalRange = 0.5f;
    public float verticalSpeed = 0.2f;
    public float startingY;
    public float multiplier = 1;
    void Start()
    {
        startingY = transform.position.y;
    }
    void Update()
    {
        transform.Translate(0, verticalSpeed * Time.deltaTime * multiplier, 0);

        if (transform.position.y > startingY + verticalRange)
        {
            multiplier = -1;
        }
        else if (transform.position.y < startingY - verticalRange)
        {
            multiplier = 1;
        }
    }
}
