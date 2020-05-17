using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float jumpPower = 4;
    public float health = 100;

    public GameObject respawnEffect;

    Animator anim;
    Animation redFlash;

    float maxSpeed = 5;
    float maxJump = 6;

    Rigidbody2D rigid;
    float horizontalMove;
    float damageTimeDelay;

    bool isGrounded;
    bool isDamage;
    bool isJump;
    bool isDead;

    void Start()
    {
        isDead = false;
        health = 100;
        Instantiate(respawnEffect, transform.position, transform.rotation);

        rigid = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        redFlash = GetComponent<Animation>();
    }

    void Update()
    {
        
        horizontalMove = Input.GetAxis("Horizontal");
        _Move(horizontalMove);

        AnimationControl();

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
        {
            _Jump();
        }

        if (damageTimeDelay > 0)
        {
            damageTimeDelay -= Time.deltaTime;
        }
        else isDamage = false;
    }
    void FixedUpdate()
    {
        _LimitVelocity();
    }

    public void _Move(float horizontalMove)
    {
            rigid.velocity = new Vector2(horizontalMove * moveSpeed, rigid.velocity.y);
    }

    public void _Jump()
    {
        if (isGrounded)
        {
            rigid.velocity = Vector2.up * jumpPower;
            isGrounded = false;
            isJump = true;
        }
        else if(isJump)
        {
            rigid.velocity = Vector2.up * jumpPower * 0.8f;
            isJump = false;
        }
    }
    void _LimitVelocity()
    {
        if(rigid.velocity.x > maxSpeed) { rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); }
        else if(rigid.velocity.x < -maxSpeed) { rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y); }

        if(rigid.velocity.y > maxJump) { rigid.velocity = Vector2.up * maxJump; }
    }

    void AnimationControl()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(horizontalMove));
        if (isDamage)
        {
            redFlash.Play();
        }
    }

    void _Damage(string obj)
    {
        if(obj == "EnemyBullet" && !isDamage)
        {
            SoundManager.soundManager.PlayAudio("GruntVoice");

            ControlCameraShake.controlCameraShake._DamageShake();
            isDamage = true;
            damageTimeDelay = 0.3f;

            health -= 10;
            if (health <= 0)
                Death();
        }
        else if(obj == "BoomEnemy")
        {
            SoundManager.soundManager.PlayAudio("GruntVoice");

            isDamage = true;
            damageTimeDelay = 0.5f;

            health -= 30;
            if (health <= 0)
                Death();
        }
        else if (obj == "BossBullet")
        {
            SoundManager.soundManager.PlayAudio("GruntVoice");

            isDamage = true;
            damageTimeDelay = 1f;

            health -= 30;
            if (health <= 0)
                Death();
        }
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != "EnemyBullet")
        {
            SoundManager.soundManager.PlayAudio("Landing");
            isGrounded = true;
            isJump = false;
        }
        if (obj.tag == "DeadZone")
            Death();
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.tag != "EnemyBullet")
        {
            isGrounded = true;
            isJump = false;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        if(obj.tag != "EnemyBullet")
            isGrounded = false;
    }
    void Death()
    {
        if (!isDead)
        {
            isDead = true;
            SoundManager.soundManager.PlayAudio("DeathVoice");
            GameController.gameController._PlayerDead(gameObject);
        }
    }
}
