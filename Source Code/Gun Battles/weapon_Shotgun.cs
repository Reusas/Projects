using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_Shotgun : MonoBehaviour
{
    public int AmmoCount;
    private Text AmmoText;
    private string ammoText;
    private bool canSpawnDefaultGun = false;
    private Transform firePoint;
    private Transform firePoint2;
    private Transform firePoint3;
    public GameObject bullet;
    public Canvas gameScreen;
    private AudioSource ac;
    public float bulletSpeed;
    public AudioClip uzi;
    public float TimeBetweenShot;
    public bool canShoot3 = true;
    public bool hasShot = false;
    int enemyJumpRandomFactor;
   public Vector3 shotgunTravel;
    public Vector3 shotgunTravel2;
    public Transform player;
    void Start()
    {
        firePoint = transform.Find("FirePoint").GetComponent<Transform>();
        firePoint2 = transform.Find("FirePoint2").GetComponent<Transform>();
        firePoint3= transform.Find("FirePoint3").GetComponent<Transform>();
        ac = GetComponent<AudioSource>();
        gameScreen = GameObject.Find("Canvas").GetComponent<Canvas>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;
    }


    void Update()
    {
        ammoText = AmmoCount.ToString();
        AmmoText.text = "Ammo: " + ammoText;
        if (player.transform.rotation.y == 1)
        {
            shotgunTravel.x = -1;
            shotgunTravel2.x = -1;
        }

        if (player.transform.rotation.y == 0)
        {
            shotgunTravel.x = 1;
            shotgunTravel2.x = 1;
        }
        if (Input.GetMouseButtonDown(0) && canShoot3 == true)
        {
            canShoot3 = false;
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
        GameObject bul2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation);
        bul2.transform.parent = gameScreen.transform;
        GameObject bul3 = Instantiate(bullet, firePoint3.position, firePoint3.rotation);
        bul3.transform.parent = gameScreen.transform;
        Rigidbody2D rb = bul.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = bul2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = bul3.GetComponent<Rigidbody2D>();
        ac.PlayOneShot(uzi);
        rb.transform.Rotate(0, 0, 12);
        rb.velocity = shotgunTravel * bulletSpeed;       
        rb2.velocity = transform.right * bulletSpeed;
        rb3.transform.Rotate (0,0,-12);
        rb3.velocity = shotgunTravel2 * bulletSpeed;
        Destroy(bul, 2f);
        Destroy(bul2, 2f);
        Destroy(bul3, 2f);
        StartCoroutine(TimeBetweenShots());
        AmmoCount--;
    }




    IEnumerator TimeBetweenShots()
    {

        yield return new WaitForSeconds(TimeBetweenShot);
        canShoot3 = true;
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