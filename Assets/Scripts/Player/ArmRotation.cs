using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public Transform player;

    Vector3 mousePosition;
    float rotationZ;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>();

        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.Normalize();

        rotationZ = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (player != null)
        {
            if (mousePosition.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, rotationZ);
                player.localScale = new Vector3(1, 1, 1);
            }
            if (mousePosition.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180);
                player.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
