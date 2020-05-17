using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyAI : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public float enemySpeed;
    public float timeToShoot;
    public float maxXToShoot;
    public float maxYToShoot;
    public GameObject enemyBullet;
    public Transform player;

    Transform enemyFirePoint;
    Vector3 direction;
    EnemyController enemyController;
    float shootTimeDelay = 0;

    float distanceX;
    float distanceY;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        direction = GetRandomPosition();

        enemyFirePoint = transform;
    }

    void Update()
    {

        if (transform.position != direction && enemyController.damageTimeDelay <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, enemySpeed * Time.deltaTime);
        }
        else
            direction = GetRandomPosition();

        Flip();
        if (player != null)
        {
            distanceX = (transform.position.x - player.position.x);
            distanceY = (transform.position.y - player.position.y);
            if ((distanceX > 0 && transform.localScale.x == 1) || (distanceX < 0 && transform.localScale.x == -1))
            {
                if (shootTimeDelay <= 0 && enemyController.damageTimeDelay <= 0 && Mathf.Abs(distanceX) <= maxXToShoot && Mathf.Abs(distanceY) <= maxYToShoot)
                {
                    Shoot();
                    shootTimeDelay = timeToShoot;
                }
                else
                    shootTimeDelay -= Time.deltaTime;
            }
        }
        else if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }
    Vector3 GetRandomPosition()
    {
        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        return new Vector3(posX, posY, transform.position.z);
    }
    void Shoot()
    {
        Destroy(Instantiate(enemyBullet, enemyFirePoint.position, enemyFirePoint.rotation),6f);
    }
    void Flip()
    {
        if (direction.x - transform.position.x > 0)
            transform.localScale = new Vector2(-1, 1); 
        else transform.localScale = Vector2.one;
    }
}
