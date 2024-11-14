using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public RawImage image;
    public float scrollSpeed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(scrollSpeed, 0.01f) * Time.deltaTime, image.uvRect.size);
    }
}
