using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class EnemyAI : MonoBehaviourPunCallbacks
{

    public enum EnemyType
    {
        KnifeEnemy,
        ShooterEnemy,


    }

    public EnemyType eT;





    public float speed, health,offset,damage,knifeSpeed;
    PhotonView pV;
    Rigidbody2D rb;
    [SerializeField] GameObject healthBar;
    Slider healthSlider;
    [SerializeField] GameObject parent;
    [SerializeField] Transform knife;
    AudioSource aS;
    Collider2D col;
    LineRenderer lR;

    bool canShoot=true;

    float t;

    bool increase;

    int layer = 1 << 8;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSlider = healthBar.GetComponentInChildren<Slider>();
        pV = GetComponent<PhotonView>();
        aS = GetComponent<AudioSource>();
        lR = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        movement();
        if (eT == EnemyType.KnifeEnemy)
        {
            knifeSwing();
        }



    }

    void knifeSwing()
    {
        knife.localPosition = new Vector2(Mathf.Lerp(0,0.1f,t), 0);

        if (t <= 0)
        {
            increase = true;
        }

        if (increase == true)
        {
            t += Time.deltaTime * knifeSpeed;
        }

        if (t >= 1)
        {
            increase = false;
        }
        if (increase == false)
        {
            t -= Time.deltaTime * knifeSpeed;

        }
    }

    void movement()
    {

        healthBar.transform.position =new Vector2(transform.position.x,transform.position.y+offset);
        if (col == null)
        {
            
            col = Physics2D.OverlapBox(transform.position, new Vector2(50, 50), 360,layer);
            
        }

        if (col != null)
        {

            Vector3 pos = transform.position;
            Vector3 dir = col.transform.position - pos;
            float dist = Vector2.Distance(transform.position, col.transform.position);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            rb.velocity = transform.right * speed * Time.deltaTime;

            if (dist >= 7)
            {
                col = null;
            }

            if (eT == EnemyType.ShooterEnemy)
            {
                if (dist < 4)
                {
                    shoot();
                }
            }


        }
        else
        {
            rb.velocity = new Vector2(0, 0);

        }
    }


    public void takeDamage(float dmg)
    {
        pV.RPC("RPC_takeDamage", RpcTarget.All, dmg);
    }


    [PunRPC]
     void RPC_takeDamage(float dmg)
    {
        health -= dmg;
        pV.RPC("updateHealth", RpcTarget.All);

        if (health <= 0)
        {




            if (pV.IsMine)
            {

                Spawner sP = GameObject.Find("Spawner").GetComponent<Spawner>();
                sP.enemiesAlive--;
                sP.updateEnemies();
                


                PhotonNetwork.Destroy(parent);
            }

        }
    }


    public void shoot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (canShoot)
            {
                pV.RPC("syncAudio", RpcTarget.All);

                RaycastHit2D hit = Physics2D.Raycast(knife.position, transform.right, 10);


                if (hit && hit.transform.tag != "Zone")
                {
                    pV.RPC("showLine", RpcTarget.All, (Vector3)hit.point);
                    PlayerController en = hit.transform.GetComponent<PlayerController>();
                    if (en != null)
                    {
                        en.takeDamage(damage);
                    }
                }
                else
                {
                    pV.RPC("showLine", RpcTarget.All, transform.position + transform.right * 10);
                }

                pV.RPC("disableShot", RpcTarget.All);
                StartCoroutine(enableShooting());
            }
        }






    }


    [PunRPC]
    void disableShot()
    {
        canShoot = false;
    }


    IEnumerator disableLine()
    {
        yield return new WaitForSeconds(.1f);
        lR.enabled = false;
    }

    IEnumerator enableShooting()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    [PunRPC]
    public void showLine(Vector3 position)
    {
        lR.enabled = true;
        lR.SetPosition(0, knife.position);
        lR.SetPosition(1, position);
        StartCoroutine(disableLine());
    }


    [PunRPC]
    void syncAudio()
    {



        aS.PlayOneShot(aS.clip);

    }



    [PunRPC]
    void updateHealth()
    {
        healthSlider.value = health;
    }
}
