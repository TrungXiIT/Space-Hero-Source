using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletTrail;
    public float thrustPower;

    public GameObject player;
    public GameObject bullet;

    Rigidbody2D rigid;
    float shootTimeDelay;
    float maxShootTimeDelay = 0.1f;

    void Start()
    {
       if(firePoint == null){ firePoint = transform.Find("FirePoint"); }

       if(player == null) { player = GameObject.FindGameObjectWithTag("Player"); }

       rigid = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(shootTimeDelay > 0) { shootTimeDelay -= Time.deltaTime; }

        if (Input.GetKey(KeyCode.Mouse0) && shootTimeDelay <= 0)
        {
            _Shoot();
            shootTimeDelay = maxShootTimeDelay;
        }
    }
    void _Shoot()
    {
        Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        _Thrust();
        Destroy(Instantiate(bulletTrail, firePoint.position, firePoint.rotation), 0.05f);
        Destroy(Instantiate(bullet, firePoint.position, firePoint.rotation), 2f);
    }
    void _Thrust()
    {
        ControlCameraShake.controlCameraShake._ShootShake();
        rigid.AddForce(new Vector2(thrustPower * -player.transform.localScale.x, 0));
    }
}
