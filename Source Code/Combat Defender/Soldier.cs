using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public AudioClip[] clips;
    public GameObject hitFx;

    Rigidbody rb;
    Transform helicopter;
    Animator anim;
    AudioSource aS;
    PlayerController pC;
    Helicopter hC;
    WaveSpawner wS;
    public BoxCollider[] bodyParts;
    public GameObject[] drop;
    BoxCollider helCol;

    public float speed=500;
    public float health = 100;
    public int damage;
    public float distanceUntilShoot;
    public int moneyValue = 10;
    public bool isRocket = false;

    float distanceBetween;
    float distanceBetweenPlayer;
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();
        helicopter = GameObject.Find("Heli").GetComponent<Transform>();
        hC = helicopter.GetComponent<Helicopter>();
        helCol = helicopter.GetComponent<BoxCollider>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
        wS = GameObject.Find("SpawnPoint").GetComponent<WaveSpawner>();
    }

    
    void Update()
    {
        if (isDead == false)
        {

            distanceBetween = Vector3.Distance(transform.position, helCol.ClosestPoint(transform.position));
            distanceBetweenPlayer = Vector3.Distance(transform.position, pC.transform.position);
            aS.volume = 1 - (distanceBetweenPlayer / 100);

            if (distanceBetween > distanceUntilShoot)
            {
                Move();
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                anim.SetBool("Walk", false);
                anim.SetBool("Shoot", true);
            }
        }

    }



    private void Move()
    {
        anim.SetBool("Walk", true);
        anim.SetBool("Shoot", false);
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            isDead = true;
            int randomDrop = Random.Range(0, 101);
            if (randomDrop <= 5)
            {
                randomDrop = Random.Range(0, 3);
                Instantiate(drop[randomDrop], transform.position, transform.rotation);             
            }
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].enabled = false;
            }
            rb.isKinematic = true;
            anim.SetTrigger("Die");
            StartCoroutine(destroyBody());
            wS.activeEnemies--;
            wS.updateRemainText();
            wS.checkWaveCompletion();


        }
    }

    public void shoot()
    {
        aS.PlayOneShot(clips[0]);
        RaycastHit hit;
        Vector3 offset = new Vector3(0, 1.5f, 0);
        if (Physics.Raycast(transform.position+offset, transform.forward,out hit, 100))
        {

            if (hit.transform.name == "Heli"&&isRocket==false)
            {
                hC.takeDamage(damage);
            }

            else if (hit.transform.name == "Heli" && isRocket == true)
            {
                hC.takeDamage(damage);
                Instantiate(hitFx, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }


    IEnumerator destroyBody()
    {
        yield return new WaitForSeconds(5f);
        Destroy(transform.gameObject);
    }
}
