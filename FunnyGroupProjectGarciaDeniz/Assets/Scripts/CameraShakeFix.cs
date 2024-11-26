using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeFix : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {

        transform.position = new Vector3(Mathf.Round(transform.position.x * 16) / 16, Mathf.Round(transform.position.y * 16), transform.position.z);
        
    }
}
