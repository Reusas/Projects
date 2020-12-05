using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public string WeaponName;

    Transform[] transforms;
    PlayerController pC;
    Enemy eN;
    Transform firePoint;
    Transform receiver;
    AudioSource aS;
    ParticleSystem pS;
    Text ammoText;
    Image weaponImage;
    SpriteRenderer sR;

    public enum User
    {
        Player,
        AI,
    }

    public User user;
    public GameObject bulletPrefab;
    public GameObject casePrefab;

    public bool isAutomatic = false;
    bool canShoot = true;

    public int fireRate;
    public int ammo;
    public bool unlimitedAmmo = false;
    public int isLong = 0;
    public int weaponForce=5;

    //Lerp stuff
    float lerpRot = 10f;
    float lerpTime;
    bool shouldLerp = false;
    bool lerpBack = false;

    public int bulletDirection;

    //Ememy Stuff
    float time;
    float count;
    float restTime;


    private void Start()
    {

        pC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        eN = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        ammoText = GameObject.Find("WeaponGUI").GetComponentInChildren<Text>();
        weaponImage = GameObject.Find("WeaponGUI").GetComponentInChildren<Image>();
        sR = GetComponent<SpriteRenderer>();
        transforms =GetComponentsInChildren<Transform>();
        aS = GetComponent<AudioSource>();
        pS = GetComponentInChildren<ParticleSystem>();
        if (user == User.Player)
        {
            pC.setIndex(isLong);
            setImage();
            updateAmmo();
        }
        else
        {

            StartCoroutine(shootInterval());
            
        }


        foreach(Transform t in transforms)
        {
            if (t.transform.name=="FirePoint")
            {
                firePoint = t;
            }
            if (t.transform.name == "Receiver")
            {
                receiver = t;
            }
        }
    }

    void Update()
    {



        //Checking if gun is full auto or semi auto
        if (user == User.Player)
        {
            if (isAutomatic == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shoot();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    shoot();
                }
            }
        }


        //Lerping rotation to look like shooting
        if (shouldLerp == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0,lerpRot,lerpTime));

            if (lerpTime < 1)
            {
                lerpTime += fireRate/50 * Time.deltaTime;
            }
            else
            {
                shouldLerp = false;
                lerpBack = true;
                lerpTime = 0;
            }     
        }
        if (lerpBack == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(lerpRot, 0, lerpTime));
            if (lerpTime < 1)
            {
                lerpTime += fireRate / 50 * Time.deltaTime;
            }
        }


    }

    void shoot()
    {
        if (canShoot == true)
        {
            if (ammo != 0)
            {
                canShoot = false;
                if (unlimitedAmmo == false)
                {
                    ammo--;
                    if(user==User.Player)
                    updateAmmo();
                }

               GameObject bul= Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                Bullet b=bul.GetComponent<Bullet>();
                b.force = weaponForce;
                Debug.Log(b.force);
                Instantiate(casePrefab, receiver.transform.position, receiver.transform.rotation);
                pS.Play();
                aS.PlayOneShot(aS.clip);
                shouldLerp = true;
                StartCoroutine(enableShooting());

                //Dir
                if (user == User.Player)
                {
                    if (pC.transform.rotation.y == 1)
                    {
                        bulletDirection = 0;
                    }
                    else if (pC.transform.rotation.y == 0)
                    {
                        bulletDirection = 1;
                    }
                    dodgeBulletAI();
                }
                else
                {
                    if (eN.transform.rotation.y == 1)
                    {
                        bulletDirection = 0;
                    }
                    else if (eN.transform.rotation.y == 0)
                    {
                        bulletDirection = 1;
                    }
                }

            }
            else
            {
                if (user == User.Player)
                {
                    pC.givePistol();
                }
                else if (user == User.AI)
                {
                    eN.givePistol();
                }
                Destroy(gameObject);
            }

        }

 
       

    }

    IEnumerator enableShooting()
    {
        yield return new WaitForSeconds(60f/fireRate);
        canShoot = true;

    }

    void updateAmmo()
    {
        ammoText.text = ammo.ToString();
    }

    void setImage()
    {
        weaponImage.sprite = sR.sprite;
        if (isLong == 1)
        {
            weaponImage.transform.localScale = new Vector3(1.5f, 0.6f, 1);
            weaponImage.rectTransform.anchoredPosition = new Vector3(27.75f, 0, 0);
        }
        else
        {
            weaponImage.transform.localScale = new Vector3(1,1,1);
            weaponImage.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
    }

    //ENEMY SHOOTING SCRIPT BEGINS HERE



    IEnumerator shootInterval()
    {
        if (isAutomatic == false)
        {
             time = Random.Range(0.35f, 1f);
             count = Random.Range(1, 5);
        }
        else
        {
            time = Random.Range(0.1f, .3f);
            count = Random.Range(3, 11);
        }

       // Debug.Log("Time: "+time);
      //  Debug.Log("Count: "+count);



        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(time);
            shoot();
        }
        restTime = Random.Range(.5f, 2f);
        yield return new WaitForSeconds(restTime);
        StartCoroutine(shootInterval());


    }



    //AI STUFFFFFFFF

    void dodgeBulletAI()
    {
        int x = Random.Range(1, 101);

        if (eN.dodgeChance > x)
        {
            eN.startJump();
        }

       
    }

}
