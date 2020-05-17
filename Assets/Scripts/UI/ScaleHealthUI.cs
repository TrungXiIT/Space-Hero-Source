using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHealthUI : MonoBehaviour
{
    public Transform player;
    
    void Update()
    {
        transform.localScale = new Vector3(player.localScale.x , transform.localScale.y);
    }
}
