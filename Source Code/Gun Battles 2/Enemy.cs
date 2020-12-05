using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    PlayerController pC;
    Text livesText;
    Transform spawnLocation;
    Events eV;
    Animator anim;
    public GameObject pistol;
    Transform wepHolder;
    Transform item;

    Rigidbody2D rb;




    public float speed;
    public float distanceLimit;
    public float maxSpeed = 7;
    public float distanceUntilWalkAgain=1;
    public float jumpForce;
    public int deathDepth = -20;
    public int maxJumpsAllowed;
    public int lives = 5;
    public int dodgeChance;

    // 0 for middle 1 for left 2 for right
    public int positionInMap = 0;
    public int jumpCount;


    public bool moveLEFT;
    public bool moveRIGHT;

    bool canStop;
    public bool isEscaping = false;
    public bool shouldJump;

    public bool goToPackage = false;


    private void Start()
    {
        eV = GameObject.Find("Events").GetComponent<Events>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spawnLocation = GameObject.FindGameObjectWithTag("SpawnLocation").GetComponent<Transform>();
        livesText = GameObject.FindGameObjectWithTag("EnemyLives").GetComponent<Text>();
        wepHolder = GameObject.FindGameObjectWithTag("WeaponHolderEnemy").GetComponent<Transform>();
    }

    private void Update()
    {
        checkHeight();
        checkDeath();
        if (moveLEFT == true)
        {
            moveLeft();
        }
        else if (moveRIGHT == true)
        {
            moveRight();
        }
        else if(canStop==true)
        {
            canStop = false;
            stopMoving();
        }

        float distance = Vector2.Distance(transform.position, pC.transform.position);
        if (isEscaping == false&&goToPackage==false)
        {
            if (distance > distanceLimit)
            {
                if (pC.transform.position.x < transform.position.x)
                {
                    moveLEFT = true;
                    moveRIGHT = false;
                }
                else if (pC.transform.position.x > transform.position.x)
                {
                    moveRIGHT = true;
                    moveLEFT = false;
                }
            }
            else if (distance < distanceLimit && distance > distanceUntilWalkAgain)
            {
                moveLEFT = false;
                moveRIGHT = false;
            }
            else if (distance < distanceLimit && distance < distanceUntilWalkAgain)
            {
                isEscaping = true;
            }
        }
        else if (isEscaping == true)
        {

            escape();
        }
        else if (goToPackage == true)
        {
            GoToPackage();
        }
        





    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            shouldJump = false;
            jump();
        }
    }


    public void startJump()
    {
        if (jumpCount < maxJumpsAllowed + 1)
        {
            shouldJump = true;

        }
    }

    void moveLeft()
    {


        float movement = speed * Time.deltaTime;
        if (rb.velocity.x > -maxSpeed)
        {
            rb.AddForce(new Vector2(-movement, 0), ForceMode2D.Impulse);
        }

        transform.rotation = new Quaternion(0, 180, 0, 0);
        canStop = true;
        anim.SetBool("Walk", true);

    }

    void moveRight()
    {
        float movement = speed * Time.deltaTime;    
        if (rb.velocity.x < maxSpeed)
        {
            rb.AddForce(new Vector2(movement, 0), ForceMode2D.Impulse);
        }
        transform.rotation = new Quaternion(0, 0, 0, 0);
        canStop = true;
        anim.SetBool("Walk", true);
    }
    void stopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        anim.SetBool("Walk", false);
    }

    void jump()
    {
        jumpCount++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        anim.SetBool("Jump", true);
    }



    void escape()
    {
        float distance = Vector2.Distance(transform.position, pC.transform.position);
        if (distance < distanceLimit&&positionInMap==0)
        {
            moveLEFT = true;
        }
        else if (distance < distanceLimit && positionInMap == 1)
        {
            moveRIGHT = true;
        }
        else if (distance < distanceLimit && positionInMap == 2)
        {
            moveLEFT = true;
        }
        else
        {
            moveLEFT = false;
            isEscaping = false;
        }

    }

    void GoToPackage()
    {
        item = GameObject.FindWithTag("Item").GetComponent<Transform>();
        

        if (transform.position.x > item.position.x)
        {
            moveLEFT = true;
            moveRIGHT = false;
        }
        if (transform.position.x < item.position.x)
        {
            moveLEFT = false;
            moveRIGHT = true;
        }
        if (transform.position.y + 0.7f < item.position.y)
        {
            startJump();
        }




    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mid")
        {
           // Debug.Log("Mid");
            positionInMap = 0;
        }
        else if (collision.tag == "Left")
        {
           // Debug.Log("L");
            positionInMap = 1;
        }
        else if (collision.tag == "Right")
        {
          //  Debug.Log("R");
            positionInMap = 2;
        }
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
        eV.WinScreen();
        pC.enabled = false;
        Destroy(this.gameObject);
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


    public void checkHeight()
    {


        if (pC.transform.position.y > transform.position.y + 0.7f)
        {
            startJump();
        }
    }


    public void givePistol()
    {
        Instantiate(pistol, wepHolder);
    }


}
