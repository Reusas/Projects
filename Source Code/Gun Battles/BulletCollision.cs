using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour {
    public float bulletForce;
    private Rigidbody2D rb;
    int bossReflection;
    public static bool isBoss = false;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.transform.tag == "Enemy"&&isBoss==false && Enemy.enemyInvinc == false)
        {
            Destroy(transform.gameObject);
            GameObject theEnemy = GameObject.Find("Enemy");
            Enemy en= theEnemy.GetComponent<Enemy>();
            Rigidbody2D rb=en.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletForce;
 
        }
        if (col.transform.tag == "Enemy" && isBoss == true&&Enemy.enemyInvinc==false)
        {
            Destroy(transform.gameObject);
            GameObject theEnemy = GameObject.Find("Enemy");
            Enemy en = theEnemy.GetComponent<Enemy>();
            Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
            bossReflection = Random.Range(0, Enemy.maxRelf);
            if (bossReflection <=10)
            {
                rb.velocity = transform.right * bulletForce;

            }

            else
            {
                GameObject audio = GameObject.FindGameObjectWithTag("Enemy");
                AudioSource ass = audio.GetComponent<AudioSource>();
                ass.clip = ass.clip;
                ass.PlayOneShot(ass.clip);
                if (Enemy.maxRelf > 10)
                {
                    Enemy.maxRelf -= 7;
                    
                }



            }

        }

        if (col.transform.tag == "Player"&&PlayerController.playerInvinc==false)
        {
            Destroy(transform.gameObject);
            GameObject thePlayer = GameObject.Find("Player");
            PlayerController pl = thePlayer.GetComponent<PlayerController>();
            Rigidbody2D rb = pl.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletForce;

            
        }

        if (col.transform.tag == "Menu")
        {
            Destroy(transform.gameObject);
            GameObject menu = GameObject.Find("MenuButton");
            Menu men = menu.GetComponent<Menu>();
            men.bullets++;
        }


    }

}
