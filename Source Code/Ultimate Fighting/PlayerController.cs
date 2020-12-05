using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;
    BoxCollider2D bC;
    AudioSource aS;
    public FightingController fC;

    public AudioClip[] hitClips;



    public Slider[] healthSliders;
    public Slider mainSlider;


    public float speed;
    public float highAttackCoolDown = .5f;
    public float health=200;
    public float special = 0;

    public bool player2 = false;
    public bool ninjaDeath = false;
    public bool isNinja = false;



    [HideInInspector]
    public bool canAttack = true;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        bC = GetComponent<BoxCollider2D>();
        aS = GetComponent<AudioSource>();
        healthSliders[0] = GameObject.Find("P1Health").GetComponent<Slider>();
        healthSliders[1] = GameObject.Find("P2Health").GetComponent<Slider>();
        StartCoroutine(check());



    }

    void Update()
    {
        if (player2 == true)
        {
            Movement2();
        }
        if (player2 == false)
        {
            Movement();
        }


    }

    public void updateHealthSlider()
    {
        if (player2 == false)
        {
            mainSlider = healthSliders[0];
        }
        if (player2 == true)
        {
            mainSlider = healthSliders[1];
        }
        mainSlider.value = health;
    }

    void Movement()
    {
        
        
            if (Input.GetKey(KeyCode.A) && canMove == true)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
                anim.SetBool("Walk", true);
            }

            else if (Input.GetKey(KeyCode.D) && canMove == true)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, 0);
                anim.SetBool("Walk", true);
            }

            else
            {
                rb.velocity = new Vector2(0, 0);
                anim.SetBool("Walk", false);
            }


    }

    void Movement2()
    {
        if (Input.GetKey(KeyCode.J) && canMove == true)
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
            anim.SetBool("Walk", true);
        }


         else if (Input.GetKey(KeyCode.L) && canMove == true)
        {

            rb.velocity = new Vector2(speed * Time.deltaTime, 0);
            anim.SetBool("Walk", true);
        }

        else
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("Walk", false);
        }
    }

    public void Die(int i)
    {
        if (i==0)
        {
            anim.SetTrigger("Die");
            canMove = false;
            canAttack = false;
            bC.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            anim.SetTrigger("NinjaDeath");
            canMove = false;
            canAttack = false;
            bC.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

    }

    public void knockBack(int kb)
    {
        fC.resetAttack();
        canMove = false;
        canAttack = false;
        anim.SetTrigger("GetHit");
        rb.AddForce(new Vector2(kb*Time.deltaTime, 0));
        int clipIndex = Random.Range(0, 5);
        aS.PlayOneShot(hitClips[clipIndex]);
        StartCoroutine(knockBackReset());

    }

    public void stopSpecial()
    {
        anim.SetBool("Special", false);
    }

    public IEnumerator knockBackReset()
    {
        yield return new WaitForSeconds(.25f);
        canAttack = true;
        canMove = true;
    }

    public IEnumerator enableAttack()
    {
        yield return new WaitForSeconds(.05f);
        canAttack = true;
    }

    public IEnumerator check()
    {
        yield return new WaitForSeconds(1f);
        if (isNinja == false)
        {
            PlayerController pC = GameObject.Find("Ninja(Clone)").GetComponent<PlayerController>();
            if (pC != null)
            {
                ninjaDeath = true;
            }
        }


    }




     
}
