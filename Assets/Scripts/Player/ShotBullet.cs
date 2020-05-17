using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    public float powerShoot;
    public Transform firePoint;
    public Transform playerPoint;
    public GameObject impactEffect;

    Rigidbody2D rigid;
    void Start()
    {
        playerPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity =  new Vector2((firePoint.position - playerPoint.position).x, (firePoint.position - playerPoint.position).y).normalized * powerShoot;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("Enemy"))
        {
            obj.SendMessageUpwards("_Damage", "Bullet");
        }
        if(obj.tag != "BackGround")
        {
            Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 0.3f);
            Destroy(gameObject);
        }
    }
}
