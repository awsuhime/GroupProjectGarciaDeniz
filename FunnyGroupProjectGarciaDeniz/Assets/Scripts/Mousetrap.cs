using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetrap : MonoBehaviour
{
    public float verticalRange = 0.5f;
    public float verticalSpeed = 0.2f;
    public float startingY;
    public float multiplier = 1;

    public bool idleBobbing = true;
    public bool usesNodes = false;
    public GameObject[] nodes;
    private int currentNode;
    public float nodeSpeed = 1.5f;
    public float nodeRange = 0.2f;

    void Start()
    {
        startingY = transform.position.y;
        foreach (GameObject i in nodes)
        {
            i.transform.SetParent(null);
        }
    }
    void Update()
    {
        if (idleBobbing)
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
        if (usesNodes)
        {
            Vector3 newpos = new Vector3(transform.position.x, startingY, 0);

            if (Vector3.Distance(newpos, nodes[currentNode].transform.position) < nodeRange)
            {
                currentNode++;
                if (currentNode == nodes.Length)
                {
                    currentNode = 0;
                }
            }
            else
            {
                transform.Translate((nodes[currentNode].transform.position - newpos).normalized * nodeSpeed * Time.deltaTime);
            }
        }
    }
}
