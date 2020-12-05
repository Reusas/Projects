using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerController pC;
    Rigidbody2D rb;
   public Weapon wepPlayer;
    public Weapon wepEnemy;

    public float bulletSpeed;
    public float force;

    

    //Lerp shit
    float t = 0;
    public float lerpScale = 2f;

    void Start()
    {
        wepPlayer= GameObject.Find("Player").GetComponentInChildren<Weapon>();
        wepEnemy = GameObject.Find("Enemy").GetComponentInChildren<Weapon>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * bulletSpeed * Time.deltaTime;     
        transform.localScale = new Vector3(Mathf.Lerp(1,lerpScale,t), 1, 1);
        t += 1.5f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Rigidbody2D rb=collision.GetComponent<Rigidbody2D>();
            addForceToEnemy(rb,wepPlayer.bulletDirection);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            addForceToEnemy(rb, wepEnemy.bulletDirection);

            Destroy(gameObject);
        }



    }

    void addForceToEnemy(Rigidbody2D rb,int dir)
    {

        if (dir == 1)
        {
            // rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
            rb.velocity += new Vector2(force, 0);
        }
        else
        {
            // rb.AddForce(new Vector2(-force, 0), ForceMode2D.Impulse);
            rb.velocity += new Vector2(-force, 0);
        }



    }


    
}
