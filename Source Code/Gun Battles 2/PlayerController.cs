using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Events eV;
    Rigidbody2D rb;
    Transform wepHolder;
    Animator anim;
    public GameObject pistol;
    Transform spawnLocation;
    Text livesText;

    public float speed;
    public float jumpForce;
    public float maxSpeed;

    public int maxJumpsAllowed = 3;
    public int lives = 5;
    public int jumpCount = 0;
    int animationIndex = 0;
    bool canStop;
    public int deathDepth = -20;

    bool shouldJump;

    private void Start()
    {
        eV = GameObject.Find("Events").GetComponent<Events>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        wepHolder = GameObject.FindGameObjectWithTag("WeaponHolderPlayer").GetComponent<Transform>();
        spawnLocation = GameObject.FindGameObjectWithTag("SpawnLocation").GetComponent<Transform>();
        livesText = GameObject.FindGameObjectWithTag("PlayerLives").GetComponent<Text>();
    }

    private void Update()
    {
        movement();
        checkDeath();
    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            shouldJump = false;
            jump();
        }
    }

    private void movement()
    {
        float movement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        // rb.velocity = new Vector2(movement, rb.velocity.y);
 

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            if (rb.velocity.x > -maxSpeed)
            {
                rb.AddForce(new Vector2(movement, 0), ForceMode2D.Impulse);
            }

        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            if (rb.velocity.x < maxSpeed)
            {
                rb.AddForce(new Vector2(movement, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            walkAnimation();
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("WalkL", false);
            if (canStop == true)
            {
                canStop = false;
                //rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount<maxJumpsAllowed+1)
        {
            shouldJump = true;
           // jumpCount++;
           // rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
           // jumpAnimation();
        }
        
    }


    void jump()
    {
        jumpCount++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        jumpAnimation();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            jumpCount = 0;
            anim.SetBool("Jump", false);
            anim.SetBool("JumpL", false);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            jumpCount++;

        }
    }

    public void givePistol()
    {
        Instantiate(pistol, wepHolder);
    }

    void walkAnimation()
    {
        
        if (animationIndex == 0)
        {
            anim.SetBool("Walk", true);
        }
        else if (animationIndex == 1)
        {
            anim.SetBool("WalkL", true);
        }
    }

    void jumpAnimation()
    {
        if (animationIndex == 0)
        {
            anim.SetBool("Jump", true);
        }
        else if (animationIndex == 1)
        {
            anim.SetBool("JumpL", true);
        }
    }

    public void setIndex(int index)
    {
        animationIndex = index;
    }

    public void checkDeath()
    {
        if (transform.position.y < deathDepth)
        {
            Destroy(wepHolder.GetChild(0).gameObject);
            givePistol();


            transform.position = spawnLocation.position;
            lives--;
            updateHealthUI();
            if (lives <= 0)
            {
                die();
            }
        }
    }

   public void updateHealthUI()
    {
        livesText.text = "O " + lives.ToString();
    }

    void die()
    {
        eV.LossScreen();
        
        Destroy(this.gameObject);
    }
}
