using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCameraShake : MonoBehaviour
{
    public static ControlCameraShake controlCameraShake;

    [Header("Duration")]
    public float shootShakeDuration;
    public float killShakeDuration;
    public float bossAttackShakeDuration;
    [Header("Amplitude")]
    public float damageShakeAmplitude;
    public float shootShakeAmplitude;
    public float killShakeAmplitude;
    public float bossAttackShakeAmplitude;

    [Header("Camera Move")]

    public float smoothCameraMove;
    public float maxY, minY;

    bool isKillShake, isShootShake, isDamageShake;
    float shakeElapsedTime = 0;

    Vector3 originPosition;
    Quaternion originRotation;
    Vector2 velocity = Vector2.zero;
    Transform player;

    void Start()
    {
        controlCameraShake = GetComponent<ControlCameraShake>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (player != null)
        {
            if (shakeElapsedTime > 0)
            {
                originPosition = player.position;
                originRotation = player.rotation;
                if (isKillShake)
                {
                    transform.position = originPosition + (new Vector3(Random.Range(0, killShakeAmplitude), Random.Range(-killShakeAmplitude, killShakeAmplitude), -10));
                }
                if (isShootShake)
                {
                    transform.position = originPosition + (new Vector3(Random.Range(0, shootShakeAmplitude), Random.Range(-shootShakeAmplitude, shootShakeAmplitude), -10));
                }

                if (isDamageShake)
                {
                    transform.position = originPosition + (new Vector3(Random.Range(0, damageShakeAmplitude), Random.Range(-damageShakeAmplitude, damageShakeAmplitude), -10));
                }
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                float posX = player.position.x;
                float posY = player.position.y;

                transform.position = new Vector3(posX, posY, -10);
                transform.rotation = player.rotation;
            }
            if (transform.position.y > maxY)
                transform.position = new Vector3(transform.position.x, maxY, -10);
            else if (transform.position.y < minY)
                transform.position = new Vector3(transform.position.x, minY, -10);
        }
        else if(GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
    public void _ShootShake()
    {
        isShootShake = true;
        shakeElapsedTime = shootShakeDuration;
    }
    public void _KillShake()
    {
        isKillShake = true;
        shakeElapsedTime = killShakeDuration;
    }
    public void _DamageShake()
    {
        isDamageShake = true;
        shakeElapsedTime = shootShakeDuration;
    }
    public void _BossAttackShake()
    {
        isDamageShake = true;
        shakeElapsedTime = bossAttackShakeDuration;
    }
}
