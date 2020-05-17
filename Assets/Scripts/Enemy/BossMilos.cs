using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMilos : MonoBehaviour
{
    public float minX, maxX;
    public float timeBetweenAttack;
    public float timeBetweenWait;
    public float enemySpeed;
    public float spawnPosition;

    public GameObject flashFullScreen;
    public GameObject bossBullet;
    public Transform[] enemyAttackPoint;

    Vector3 direction;

    float timeAttackDelay;
    bool isAttack;

    Animator anim;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, spawnPosition, transform.position.z);
        Destroy(Instantiate(flashFullScreen, Vector3.zero, Quaternion.identity), 2.5f);
        anim = GetComponent<Animator>();
        timeAttackDelay = timeBetweenAttack;
        direction = GetRandomPosition();
    }

    void Update()
    {
        anim.SetBool("isAttack", isAttack);

        if (timeAttackDelay > 0)
        {
            isAttack = false;
            timeAttackDelay -= Time.deltaTime;
        }
        else if(timeAttackDelay <= 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }

        if (transform.position == direction)
        {
            direction = GetRandomPosition();
        }
        else Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, enemySpeed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        isAttack = true;
        ControlCameraShake.controlCameraShake._BossAttackShake();
        yield return new WaitForSeconds(0.5f);
        Destroy(Instantiate(flashFullScreen, Vector3.zero, Quaternion.identity), 1.5f);
        for (int i = 0; i < enemyAttackPoint.Length; i++)
        {
            Destroy(Instantiate(bossBullet, enemyAttackPoint[i].position, enemyAttackPoint[i].rotation), 10);
        }

        yield return new WaitForSeconds(timeBetweenAttack);
        timeAttackDelay = timeBetweenWait;
    }
    Vector3 GetRandomPosition()
    {
        float posX = Random.Range(minX, maxX);
        return new Vector3(posX, transform.position.y, transform.position.z);
    }
    void OnDestroy()
    {
        GameController.gameController.Scores += Random.Range(4500, 5000);
    }
}
