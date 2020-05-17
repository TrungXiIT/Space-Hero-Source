using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyAI : MonoBehaviour
{
    EnemyController enemyController;
    
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == "Player")
        {
            GameController.gameController.Scores += 50;
           
            obj.SendMessageUpwards("_Damage", "BoomEnemy");
            enemyController._Dead();
        }
    }
}
