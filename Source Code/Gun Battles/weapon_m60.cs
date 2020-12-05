using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_m60 : MonoBehaviour
{
    public int AmmoCount;
    private Text AmmoText;
    private string ammoText;
    private bool canSpawnDefaultGun = false;
    private Transform firePoint;
    public GameObject bullet;
    public Canvas gameScreen;
    private AudioSource ac;
    public float bulletSpeed;
    public AudioClip uzi;
    public float TimeBetweenShot;
    public bool canShoot1 = true;
    public bool hasShot = false;
    int enemyJumpRandomFactor;
    int shotAudio;
    void Start()
    {
        firePoint = transform.Find("FirePoint").GetComponent<Transform>();
        ac = GetComponent<AudioSource>();
        gameScreen = GameObject.Find("Canvas").GetComponent<Canvas>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;
    }


    void Update()
    {
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;

        if (Input.GetMouseButton(0) && canShoot1 == true)
        {

            canShoot1 = false;
            hasShot = true;
            Shoot();
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy en = enemy.GetComponent<Enemy>();
            enemyJumpRandomFactor = Random.Range(0, 2);
            if (enemyJumpRandomFactor == 1)
            {
                
                en.Jump();
            }

        }

        if (AmmoCount == 0)
        {
            StartCoroutine(waitForGun());
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
        StartCoroutine(TimeBetweenShots());
        AmmoCount--;
    }

    IEnumerator TimeBetweenShots()
    {

        yield return new WaitForSeconds(TimeBetweenShot);
        canShoot1 = true;
        hasShot = false;
    }
    IEnumerator waitForGun()
    {
        yield return new WaitForSeconds(TimeBetweenShot);
        canSpawnDefaultGun = true;
        GameObject ch = GameObject.Find("Chest");
        Chest chs = ch.GetComponent<Chest>();
        chs.GiveDefaultGun();



    }
}