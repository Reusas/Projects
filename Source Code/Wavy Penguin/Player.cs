using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    GameManager gm;

    public AudioSource aS;
    public AudioClip[] aC;

    int upAngle = 45;
    int downAngle = -45;

    public int speed=10;

    public Score sc;

    public static int highScore = 0;
    public int highsc;
    public int Coins;
    public int prevScore;
    public Text cStxt;
    public Text hStxt;

    bool hasCol = false;


    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("Canvas").GetComponent<GameManager>();
        LoadScore();
        LoadCoins();

    }


    void Update()
    {
        Movement();
        highsc = highScore;
    }

    void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y + 1.305f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            rb.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y - 1.305f);
        }
        if (Input.GetMouseButton(0))
        {
            rb.transform.rotation = new Quaternion(0, 0, upAngle, 100);
            rb.velocity = new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
        }
        else
        {
            rb.transform.rotation = new Quaternion(0, 0, downAngle, 100);
            rb.velocity = new Vector3(speed * Time.deltaTime, -speed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Icicle")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "IciclePoint"&&hasCol==false)
        {
            sc.updateText();
            hasCol = true;
            StartCoroutine(enableCol());
        }

        if (col.transform.tag == "Coin")
        {
            aS.clip = aC[1];
            aS.Play();
            Coins++;
            Destroy(col.transform.gameObject);
        }
    }

    void Die()
    {
        aS.clip = aC[0];
        aS.Play();
        gm.DeathScreen();
        cStxt.text = "Current Score: " + sc.score;
        hStxt.text = "High Score: " + highScore;
        prevScore = sc.score;
        if (prevScore > highScore)
        {
            highScore = sc.score;
            highsc = highScore;
            hStxt.text = "High Score: " + highScore;

        }
        SaveScore();
        SaveCoins();
        Destroy(transform.gameObject);

    }

    IEnumerator enableCol()
    {
        yield return new WaitForSeconds(.2f);
        hasCol = false;
    }



    public void SaveScore()
    {
        SaveSystem.SavePlayer(this);
    }

    public void SaveCoins()
    {
        SaveSystemCoins.SavePlayer(this);
    }

    public void LoadScore()
    {
        ScoreData sD = SaveSystem.LoadPlayer();
        highScore = sD.highScore;
    }

    public void LoadCoins()
    {
        CoinData sD = SaveSystemCoins.LoadPlayer();
        Coins = sD.coins;
    }

}



