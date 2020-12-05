using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed = 500;
    public int damage = 20;
    PlayerController pC;
    public GameObject blood;
    Vector3 offset;
    GameManager gM;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
        offset = new Vector2(0.2f, 0.3f);
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void bloodEfx()
    {
        Instantiate(blood, transform.position + offset, transform.rotation);
        Instantiate(blood, transform.position + offset+offset, transform.rotation);
        Instantiate(blood, transform.position + offset-offset, transform.rotation);
    }
    private void Update()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            pC = col.GetComponent<PlayerController>();
            pC.health -= damage;
            pC.updateHealthSlider();
            pC.stopSpecial();
            bloodEfx();
            Destroy(gameObject);
            if (pC.health <= 0)
            {
                pC.Die(0);
                gM.DisplayWinner(1);
                gM.shutItDown(1);
            }
            else
            {
                pC.knockBack(-50000);
            }
        }

        if (col.transform.tag == "Player2")
        {
            pC = col.GetComponent<PlayerController>();
            pC.health -= damage;
            pC.updateHealthSlider();
            pC.stopSpecial();
            bloodEfx();
            Destroy(gameObject);
            if (pC.health <= 0)
            {
                pC.Die(0);
                gM.DisplayWinner(0);
                gM.shutItDown(0);
            }
            else
            {
                pC.knockBack(50000);
            }
        }
    }
}
