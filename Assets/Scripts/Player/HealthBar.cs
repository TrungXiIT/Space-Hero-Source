using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float currentHealth = 1;
    public float maxHealth = 100;

    public PlayerController playerController;
    public Text healthIndicator;

    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        currentHealth = playerController.health / maxHealth;

        if (currentHealth <= 0.3f)
            sprite.color = Color.red;
        else sprite.color = Color.green;
        healthIndicator.text = playerController.health + "/" + maxHealth + " HP";

        if (transform.localScale.x >= 0)
            transform.localScale = new Vector3(currentHealth, transform.localScale.y);
    }
    public void SetHealth(float health)
    {
        currentHealth = health;
    }
}
