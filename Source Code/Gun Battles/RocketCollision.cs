using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    public float bulletForce;
    public GameObject RocketFX;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            Destroy(transform.gameObject);
            GameObject theEnemy = GameObject.Find("Enemy");
            Enemy en = theEnemy.GetComponent<Enemy>();
            Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletForce;
            GameObject fx = Instantiate(RocketFX, transform.position, transform.rotation);
            Destroy(fx, 4f);

        }

        /*if (col.transform.tag == "Player")
        {
            Destroy(transform.gameObject);
            GameObject thePlayer = GameObject.Find("Player");
            PlayerController pl = thePlayer.GetComponent<PlayerController>();
            Rigidbody2D rb = pl.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * bulletForce;

        }
        */
    }



}

