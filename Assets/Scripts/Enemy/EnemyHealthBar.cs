using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    float currentHealth = 1;
    public float maxHealth = 100;

    public EnemyController enemyController;
    public Text healthIndicator;

    SpriteRenderer sprite;

    Vector3 originScale;

    void Start()
    {
        originScale = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        currentHealth = ( enemyController.health / maxHealth ) * originScale.x;
        
        if(currentHealth <= 0.3f)
        {
            sprite.color = Color.red;
        }
        healthIndicator.text = enemyController.health + "/" + maxHealth + " HP";

        if (transform.localScale.x >= 0)
            transform.localScale = new Vector3(currentHealth, transform.localScale.y);
    }
    public void SetHealth(float health)
    {
        currentHealth = health;
    }
}
