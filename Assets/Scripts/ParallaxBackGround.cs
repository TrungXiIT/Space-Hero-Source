using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    public float paralaxSmooth;
    public GameObject cam;

    float length, startPos;
    
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = cam.transform.position.x * paralaxSmooth;
        transform.position = new Vector3(startPos + distance, transform.position.y);        
    }
}
