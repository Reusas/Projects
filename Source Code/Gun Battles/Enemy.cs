using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    Rigidbody2D rb;
    public GameObject player;
    public Vector2 spawn;
    private Text lives;
     public float enemyySpeed;
    public float enemySpeed;
    bool hit = false;
    public bool isGround = false;
    public bool canShoot = true;
    public float invincTime;
    public int EnemyLives;
    private string liveText;
    public Vector2 jumpForce;
    public int maxJumpChance;
    public float dist;
    int jumpShoot;
    public static int maxRelf = 101;
    public static bool enemyInvinc;
    bool zonereach = false;





    void Start () {
        rb = GetComponent<Rigidbody2D>();
        lives = GameObject.Find("ELives").GetComponent<Text>();
        liveText = EnemyLives.ToString();
        lives.text = liveText;
        maxRelf = 101;
        enemyInvinc = false;
    }
    private void Update()
    {
        liveText = EnemyLives.ToString();
        lives.text = liveText;
        GameObject thePlayer = GameObject.Find("Player");
        PlayerController pc = thePlayer.GetComponent<PlayerController>();
        dist = Vector2.Distance(pc.transform.position, transform.position);
        if (dist <= 140)
        {
            enemyySpeed = 0;

        }

        if (player.transform.position.x <= -200f && transform.position.x <= -200f|| player.transform.position.x >= 200f && transform.position.x >= 200f)
        {
            enemyySpeed = 0;
            zonereach = true;
        }
        else
        {
            zonereach = false;
        }



        if (pc.transform.position.y <= 700)
        {
            enemyySpeed = 0;
        }

        if (pc.transform.position.y > 700)
        {
            enemyySpeed = enemySpeed;
        }


        if (dist >= 140&&zonereach==false)
        {
            enemyySpeed = enemySpeed;
        }
        



        if (pc.transform.position.x > transform.position.x && hit == false && isGround == true)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = transform.right * enemyySpeed;


            
        }
        
        if (pc.transform.position.x < transform.position.x && hit == false && isGround == true)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            rb.velocity = transform.right * enemyySpeed;


        }

        if (pc.transform.position.y <= transform.position.y&&canShoot==true)
        {
            canShoot = false;
            GameObject gunai = GameObject.FindGameObjectWithTag("WeaponAI");
            WeaponAI WAI = gunai.GetComponent<WeaponAI>();
            WAI.Shoot();

            
        }

        if (transform.position.y <= -1100)
        {
            Die();
        }


    }

    void Die()
    {
        
        EnemyLives--;
        transform.position = spawn;
        Image im = GetComponent<Image>();
        im.color = new Color32(255, 255, 255, 150);
        enemyInvinc = true;
        StartCoroutine(endInvincibility());
        if (EnemyLives <= 0)
        {
            Destroy(transform.gameObject);
            GameObject gm = GameObject.Find("GameManager");
            GameManager GameMan = gm.GetComponent<GameManager>();
            GameMan.Win();
            GameObject thePlayer = GameObject.Find("Player");
            PlayerController pc = thePlayer.GetComponent<PlayerController>();
            Destroy(thePlayer);
        }
    }

    public void Jump()
    {
        if (isGround == true)
        {
           isGround = false;
           rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
            jumpShoot = Random.Range(0, maxJumpChance);
            if (jumpShoot == 1&&canShoot==true)
            {
                isGround = false;
                GameObject gunai = GameObject.FindGameObjectWithTag("WeaponAI");
                WeaponAI WAI = gunai.GetComponent<WeaponAI>();
                WAI.Shoot();
            }
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hit = true;
            StartCoroutine(hitTime());
        }


    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = false;
            
        }
    }

    
    IEnumerator hitTime()
    {
        yield return new WaitForSeconds(.3f);
        hit = false;
    }

    IEnumerator endInvincibility()
    {
        yield return new WaitForSeconds(invincTime);
        Image im = GetComponent<Image>();
        im.color = new Color32(255, 255, 255, 255);
        enemyInvinc = false;
    }




}
