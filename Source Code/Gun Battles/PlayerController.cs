using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    public float xSpeed;
    public float jumpHeight;
    public bool canJump;
    public float jumpForce;
    private bool doubleJump = false;
    public Vector2 spawn;
    public int PlayerLives;
    private Text lives;
    private string liveText;
    bool doJump = false;
    int enemyJumpRandomFactor;
    public int maxJumpRandomFactor;
    bool knockBack = false;
    public float invincTime;
    public static bool playerInvinc;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        lives = GameObject.Find("PLives").GetComponent<Text>();
        liveText = PlayerLives.ToString();
        lives.text = liveText;
        playerInvinc = false;



    }
	

	void Update () {
        liveText = PlayerLives.ToString();
        lives.text = liveText;

        if (Input.GetKey(KeyCode.D) )
        {
            rb.transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.transform.Translate(xSpeed*Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.rotation = new Quaternion(0, 180, 0,0);
           
            rb.transform.Translate(xSpeed*Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump == true&&knockBack==false)
        {           
            doJump = true;
            doubleJump = true;
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy en = enemy.GetComponent<Enemy>();
            enemyJumpRandomFactor = Random.Range(0, maxJumpRandomFactor);
            if (enemyJumpRandomFactor == 1)
            {

                en.Jump();
            }


        }


        if (Input.GetKeyDown(KeyCode.W) && canJump == false && doubleJump==true&&knockBack==false)
        {       
            doJump = true;
            doubleJump = false;
        }
       
        if (transform.position.y <=-1100)
        {
            Die();
        }
        if (PlayerLives == 0)
        {
            GameObject gm = GameObject.Find("GameManager");
            GameManager GameMan = gm.GetComponent<GameManager>();
            GameMan.GameOver();
            playerInvinc = false;
        }
    }

    private void FixedUpdate()
    {
        if (doJump == true)
        {
            doJump = false;
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag=="Ground")
        {
            canJump = true;
            doubleJump = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        knockBack = true;
        StartCoroutine(waitForKB());
        if (doubleJump == false&&canJump==true)
        {
            canJump = false;
            StartCoroutine(waitForKB());
        }



    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            canJump = false;

        }



    }

    void Die()
    {
        transform.position = spawn;
        PlayerLives--;
        StartCoroutine(waitForGun());
        Image im = GetComponent<Image>();
        im.color = new Color32(255, 255, 255, 150);
        playerInvinc = true;
        StartCoroutine(endInvincibility());
        if (PlayerLives <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    IEnumerator waitForGun()
    {
        yield return new WaitForSeconds(.1f);
        GameObject ch = GameObject.Find("Chest");
        Chest chs = ch.GetComponent<Chest>();
        chs.GiveDefaultGun();
    }
    IEnumerator waitForKB()
    {
        yield return new WaitForSeconds(.25f);
        if (canJump == false)
        {
            canJump = false;
            doubleJump = true;
            knockBack = false;
        }
        else
        {
            canJump = true;
            doubleJump = true;
            knockBack = false;
        }

        
        
    }

    IEnumerator endInvincibility()
    {
        yield return new WaitForSeconds(invincTime);
        Image im = GetComponent<Image>();
        im.color = new Color32(255, 255, 255, 255);
        playerInvinc = false;
    }
}
