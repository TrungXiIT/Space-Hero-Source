using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float health = 100;
    public float healthUIDisplayTime;
    public GameObject destroyEffect;
    public GameObject explosionEffect;
    public GameObject healthUI;

    Animation redFlash;

    float healUITimer;
    [HideInInspector]
    public float damageTimeDelay;

    void Start()
    {
        health = maxHealth;
        redFlash = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            _Dead();
        }
        if(damageTimeDelay > 0)
        {
            damageTimeDelay -= Time.deltaTime;
            redFlash.Play();
        }

        if (healUITimer > 0)
        {
            healUITimer -= Time.deltaTime;
            healthUI.SetActive(true);
        }
        else healthUI.SetActive(false);
    }
    void _Damage(string obj)
    {
        if(obj == "Bullet")
        {
            SoundManager.soundManager.PlayAudio("Hit");
            damageTimeDelay = 0.5f;
            health -= 10;

            healUITimer = healthUIDisplayTime;
        }
    }
    public void _Dead()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity),3.3f);

        SoundManager.soundManager.PlayAudio("EnemyExplosion");
        ControlCameraShake.controlCameraShake._KillShake();

        GameController.gameController.Scores += 100;
        if(maxHealth == 50)
            GameController.gameController.Scores += 50;
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        GameController.gameController.Scores += Random.Range(50,200);
    }
}
