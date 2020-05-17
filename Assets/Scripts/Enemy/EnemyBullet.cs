using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float powerShoot;
    public float powerImpact;
    
    public Transform playerPoint;
    public GameObject impactEffect;


    Transform enemyFirePoint;
    Rigidbody2D rigid;
    void Start()
    {
        playerPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();

        enemyFirePoint = transform;
        if(playerPoint != null)
            rigid.velocity = new Vector2((playerPoint.position - enemyFirePoint.position).x, (playerPoint.position - enemyFirePoint.position).y).normalized * powerShoot;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            int powerDirection;

            if (obj.transform.position.x > transform.position.x)
                powerDirection = -1;
            else powerDirection = 1;

            obj.GetComponent<Rigidbody2D>().AddForce( Vector2.left * powerDirection * powerImpact);

            obj.SendMessageUpwards("_Damage", "EnemyBullet");
        }
        if (obj.tag != "BackGround" && obj.tag != "Enemy" && obj.tag != "EnemyBullet")
        {
            SoundManager.soundManager.PlayAudio("Explosion");
            Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 0.5f);
            Destroy(gameObject);
        }
    }
}
