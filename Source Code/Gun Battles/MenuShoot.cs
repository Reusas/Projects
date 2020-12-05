using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShoot : MonoBehaviour
{

    private Transform firePoint;
    public GameObject bullet;
    public Canvas gameScreen;
    private AudioSource ac;
    public float bulletSpeed;
    public AudioClip uzi;
    public float TimeBetweenShot;
    public bool canShoot1 = true;
    public bool hasShot = false;
    bool finish = false;
    int shotAudio;
    void Start()
    {
        firePoint = transform.Find("FirePoint").GetComponent<Transform>();
        ac = GetComponent<AudioSource>();
        gameScreen = GameObject.Find("Canvas").GetComponent<Canvas>();
        finish = false;
    }


    void Update()
    {

        if (finish == false)
        {
            StartCoroutine(Shoott());
            finish = true;
        }
        

        

    }



    void Shoot()
    {

        GameObject bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bul.transform.parent = gameScreen.transform;
        Rigidbody2D rb = bul.GetComponent<Rigidbody2D>();
        ac.PlayOneShot(uzi);
        rb.velocity = transform.right * bulletSpeed;
        Destroy(bul, 2f);
    }


    IEnumerator Shoott()
    {
        yield return new WaitForSeconds(2f);
        Shoot();
        yield return new WaitForSeconds(.1f);
        Shoot();
        yield return new WaitForSeconds(.1f);
        Shoot();


    }
}