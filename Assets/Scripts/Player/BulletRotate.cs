using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotate : MonoBehaviour
{
    
    Vector3 mousePosition;
    float rotationZ;

    void Start()
    {
        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.Normalize();

        rotationZ = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
