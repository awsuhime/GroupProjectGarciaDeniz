using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTime = 3;
    float lifeStart;
    void Start()
    {
        lifeStart = Time.time;
    }

    void Update()
    {
        float lifeLeft = Time.time - lifeStart;
        if (lifeLeft > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
