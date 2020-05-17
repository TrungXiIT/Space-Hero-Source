using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float powerShoot;
    public float powerImpact;

    public GameObject impactEffect;

    Rigidbody2D rigid;
    void Start()
    {

        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = transform.up * powerShoot;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            int powerDirection;

            if (obj.transform.position.x > transform.position.x)
                powerDirection = -1;
            else powerDirection = 1;

            obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * powerDirection * powerImpact);

            obj.SendMessageUpwards("_Damage", "BossBullet");
        }
        if (obj.tag != "Enemy" && obj.tag != "EnemyBullet" && obj.tag != "BossBullet")
        {
            ControlCameraShake.controlCameraShake._ShootShake();
            SoundManager.soundManager.PlayAudio("Explosion");
            Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 3f);
            Destroy(gameObject);
        }
    }
}
