using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    PhotonView pV;
    LineRenderer lR;
    AudioSource aS;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject parent;
    Slider healthSlider;
    Respawner rS;
    Spawner sP;

    public float speed,damage,health,offset;


    float hor;
    float ver;

    bool effectActive = false;
    bool dead = false;
    public int playersAlive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pV = GetComponent<PhotonView>();
        lR = GetComponent<LineRenderer>();
        aS = GetComponent<AudioSource>();
        healthSlider = healthBar.GetComponentInChildren<Slider>();
        healthBar.transform.position = transform.position;
        rS = GameObject.Find("DeathText").GetComponent<Respawner>();

        sP = GameObject.Find("Spawner").GetComponent<Spawner>();

        sP.addPlayer("+");




    }

    private void Update()
    {
        if (pV.IsMine)
        {

            movement();
            shooting();
        }




    }

    void movement()
    {
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + offset);
        hor = Input.GetAxisRaw("Horizontal")*speed*Time.deltaTime;
        ver = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        rb.velocity = new Vector2(hor, ver);


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (angle > 90 || angle < -90)
        {
            firePoint.transform.localRotation = new Quaternion(180, 0, 0,0);
        }
        else
        {
            firePoint.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

    }

    void shooting()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right,10);
            aS.PlayOneShot(aS.clip);

            if (pV.IsMine)
            {
                pV.RPC("syncAudio", RpcTarget.All);
            }

            if (hit&&hit.transform.tag!="Zone")
            {
                pV.RPC("showLine", RpcTarget.All, (Vector3)hit.point);
                EnemyAI en = hit.transform.GetComponent<EnemyAI>();
                if (en != null)
                {
                    en.takeDamage(damage);
                }
            }
            else
            {
                pV.RPC("showLine", RpcTarget.All, transform.position + transform.right * 10);
            }
            

        }
        
    }




    [PunRPC]
    void syncAudio()
    {



        aS.PlayOneShot(aS.clip);

    }




    IEnumerator disableLine()
    {
        yield return new WaitForSeconds(.1f);
        lR.enabled = false;
    }


    [PunRPC]
    public void showLine(Vector3 position)
    {
        lR.enabled = true;
        lR.SetPosition(0, firePoint.position);
        lR.SetPosition(1, position);
        StartCoroutine(disableLine());
    }

   public void takeDamage(float dmg)
    {
        pV.RPC("RPC_takeDamage", RpcTarget.All, dmg);
    }


    public void doubleDamage()
    {
        if (effectActive == false)
        {
            effectActive = true;
            StartCoroutine(dd());

        }

    }

    public void doubleSpeed()
    {
        if (!effectActive)
        {
            effectActive = true;
            StartCoroutine(dS());

        }

    }

    IEnumerator dd()
    {
        float prevDamage = damage;
        damage = damage * 2;
        int x = Random.Range(10, 21);

        lR.startColor = Color.red;

        yield return new WaitForSeconds(x);
        damage = prevDamage;
        lR.startColor = new Color32(202, 193, 20,1);
        effectActive = false;

    }


    IEnumerator dS()
    {
        float prevSpeed = speed;
        speed = speed * 2;

        int x = Random.Range(10, 21);
        yield return new WaitForSeconds(x);
        speed = prevSpeed;
        effectActive = false;
    }

    [PunRPC]
    void RPC_takeDamage(float dmg)
    {
        health -= dmg;
        healthSlider.value = health;

        if (health <= 0)
        {
            if (pV.IsMine)
            {
                sP.addPlayer("-");
                if (sP.playersAlive > 0)
                {

                    rS.beginRespawn();
                }
                else if (sP.playersAlive <= 0)
                {
                    sP.gameOver();
                }
                PhotonNetwork.Destroy(parent);
            }


        }

    }






    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            EnemyAI en = col.transform.GetComponent<EnemyAI>();
            takeDamage(en.damage);
        }
    }
}
