using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAI : MonoBehaviour
{

    Transform firePoint;
    public GameObject bullet;
    public Canvas gameScreen;
    private AudioSource ac;
    public float bulletSpeed;
    private float TimeBetweenShot;
    public float minTimeBetweenShot;
    public float maxTimeBetweenShot;
    public AudioClip uzi;


    void Start()
    {
        firePoint = transform.Find("FirePoint").GetComponent<Transform>();
        ac = GetComponent<AudioSource>();
        
    }



    public void Shoot()
    {
       
       


            GameObject bul = Instantiate(bullet, firePoint.position, firePoint.rotation);
            bul.transform.parent = gameScreen.transform;
            Rigidbody2D rb = bul.GetComponent<Rigidbody2D>();
            ac.PlayOneShot(uzi);
            rb.velocity = transform.right * bulletSpeed;
            Destroy(bul, 2f);
            StartCoroutine(TimeBetweenShots());
        


        
    }

    IEnumerator TimeBetweenShots()
    {
        TimeBetweenShot = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        yield return new WaitForSeconds(TimeBetweenShot);
        GameObject en = GameObject.FindGameObjectWithTag("Enemy");
        Enemy EnSc = en.GetComponent<Enemy>();
        EnSc.canShoot = true;
    }


    


}
