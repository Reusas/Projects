using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Animator anim;
    AudioSource aS;
    Camera cam;
    PlayerController pC;
    Helicopter heli;
    Soldier en;
    Money mN;
    SupplyPlane sP;
    WeaponSwitcher wS;
    Shop sh;

    public GameObject muzzleFlash;
    public Image scope;
    public GameObject bulletImage;
    public GameObject hitEffect;
    public AudioClip reloadSound;
    public AudioClip shootSound;
    public AudioClip radioSound;

    Transform crossHair;
    Transform bulletList;
    Text clipText;

    GameObject[] buls=new GameObject[100];

    Vector3 bulPos = new Vector3(371f, -273f, 0);

    public float range = 100;
    public float recoil = 1;
    public float timeBetweenShots;
    public float minAccuracy;
    public float maxAccuracy;
    public int ammo;
    public int maxAmmo = 7;
    public int clips;
    public int maxClips;
    public float damage;
    public int headShotBonus;

    public int muzzleRo = -190;

    public bool fullAuto = false;
    public bool canScope = false;

    int aimInt = 0;
    int bulOffset = 5;
    public bool canShoot = false;
    public bool canReload;
    
    public bool canRadio=true;
    public bool isAim = false;
    public bool canSwitch = false;
    Vector3 offset;

    [HideInInspector]
    public bool isRecoil;

    public void enShoot()
    {
        canShoot = true;
        canSwitch = true;
    }

    void Start()
    {
        canShoot = false;
        canSwitch = false;
        anim = GetComponent<Animator>();
        cam = GetComponentInParent<Camera>();
        aS = GetComponent<AudioSource>();
        pC = GetComponentInParent<PlayerController>();
        crossHair = GameObject.Find("Crosshair").GetComponent<Transform>();
        bulletList = GameObject.Find("Bullets").GetComponent<Transform>();
        clipText = GameObject.Find("ClipText").GetComponent<Text>();
        heli = GameObject.Find("Heli").GetComponent<Helicopter>();
        mN = GameObject.Find("Money").GetComponent<Money>();
        sP = GameObject.Find("SupplyPlane").GetComponent<SupplyPlane>();
        wS = GameObject.Find("WeaponHolder").GetComponent<WeaponSwitcher>();
        Reload();
        updateClips();
        maxClips = clips;
        sh = GameObject.Find("Shop").GetComponent<Shop>();
        if (canScope == true)
        {
            crossHair.gameObject.SetActive(false);
        }
        
    }

    void Update()
    {
        if (pC.isPaused == false && sh.shopOpen==false)
        {


            if (fullAuto == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
            if (fullAuto == true)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (canShoot == true)
                {
                    Aim();
                }

            }

            if (Input.GetKeyDown(KeyCode.R) && canReload == true)
            {
                if (ammo <= 0 && clips > 0)
                {
                    anim.SetBool("Reload", true);
                    canSwitch = false;
                    clips--;
                    updateClips();
                    aS.clip = reloadSound;
                    aS.Play();
                    canReload = false;

                }
            }

        }

    }

    void Shoot()
    {
        if (canShoot == true&&ammo>0)
        {

            //Shooting
            RaycastHit hit;

            if (aimInt == 0)
            {
                float y = Random.Range(minAccuracy, maxAccuracy);
                float z = Random.Range(minAccuracy, maxAccuracy);
                offset = new Vector3(0, y, z);
            }
            else if (aimInt == 1)
            {
                float y = Random.Range(minAccuracy, maxAccuracy);
                float z = Random.Range(minAccuracy, maxAccuracy);
                offset = new Vector3(0, y/2, z/2);
            }

            //Hitting
            if (Physics.Raycast(cam.transform.position, cam.transform.forward-offset, out hit, range))
            {
                Debug.Log(hit.collider);
                if (hit.transform.tag == "Enemy")
                {
                    en = hit.transform.GetComponent<Soldier>();

                    if (en.isDead == false)
                    {
                        if (hit.collider.name == "Head")
                        {
                            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            en.takeDamage((damage * 2)*pC.damageMultiplier);
                            mN.money += (en.moneyValue * 2) + headShotBonus;
                            mN.updateMoney();
                        }

                        if (hit.collider.name == "Body")
                        {
                            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            en.takeDamage(damage);
                            mN.money += en.moneyValue;
                            mN.updateMoney();
                        }

                        if (hit.collider.name == "Legs")
                        {
                            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                            en.takeDamage((damage / 2) * pC.damageMultiplier);
                            mN.money += en.moneyValue / 2;
                            mN.updateMoney();
                        }

                        if (hit.collider.name == "TankHead")
                        {
                            en.takeDamage((damage) * pC.damageMultiplier);
                            mN.money += en.moneyValue;
                            mN.updateMoney();
                        }
                    }



                }

                if (hit.transform.name == "Heli")
                {
                    heli.takeDamage((int)damage);
                }
            }
            aS.PlayOneShot(shootSound);
            int x = Random.Range(0, 360);
            muzzleFlash.transform.localEulerAngles = new Vector3(x, muzzleRo, -90);
            muzzleFlash.SetActive(true);
            anim.SetBool("Shoot", true);
            canShoot = false;
            StartCoroutine(disableMuz());
            StartCoroutine(disableMuzFR());
            //Bullets
            Destroy(buls[ammo-1]);
            ammo--;
        }

    }

    void Aim()
    {
        if (canScope == false)
        {
            if (aimInt == 0)
            {
                isAim = true;
                anim.SetBool("Aim", true);
                pC.speed = pC.speed / 2;
                crossHair.gameObject.SetActive(false);
                aimInt = 1;
            }
            else if (aimInt == 1)
            {
                isAim = false;
                anim.SetBool("Aim", false);
                pC.speed = pC.speed*2;
                crossHair.gameObject.SetActive(true);
                aimInt = 0;

            }
        }
        else
        {
            if (aimInt == 0)
            {
                isAim = true;
                anim.SetBool("Aim", true);
                pC.speed = pC.speed / 2;
                crossHair.gameObject.SetActive(false);
                aimInt = 1;

            }
            else if (aimInt == 1)
            {
                isAim = false;
                anim.SetBool("Aim", false);
                pC.speed = pC.speed * 2;
                aimInt = 0;


            }
        }



    }

    public void scopeIn()
    {
        scope.gameObject.SetActive(true);
        Camera.main.fieldOfView = 30f;
    }

    public void scopeOut()
    {
        scope.gameObject.SetActive(false);
        Camera.main.fieldOfView = 60f;
    }


    public void Reload()
    {

            ammo = maxAmmo;
            for (int i = 0; i < ammo; i++)
            {

                buls[i] = Instantiate(bulletImage, new Vector3(bulletList.transform.position.x + bulOffset, bulletList.transform.position.y, bulletList.transform.position.z), new Quaternion(0, 0, 0, 0), bulletList.transform);
                bulOffset = bulOffset + 25;

            }
            for (int i = 0; i < ammo; i++)
            {

                bulOffset = bulOffset - 25;
            }
        
        anim.SetBool("Reload", false);
        canReload = true;
        canSwitch = true;
    }

    public void clearCanvas()
    {
        for(int i=0; i < buls.Length; i++)
        {
            Destroy(buls[i]);
        }
        //clips = 0;
        updateClips();
        bulOffset = 5;
    }

    public void drawCanvas()
    {
        bulOffset = 5;
        for (int i = 0; i < ammo; i++)
        {

            buls[i] = Instantiate(bulletImage, new Vector3(bulletList.transform.position.x + bulOffset, bulletList.transform.position.y, bulletList.transform.position.z), new Quaternion(0, 0, 0, 0), bulletList.transform);
            bulOffset = bulOffset + 25;

        }
        for (int i = 0; i < ammo; i++)
        {

            bulOffset = bulOffset - 25;
        }
    }


    public void updateClips()
    {

        clipText.text = "x" + clips.ToString();
    }

    public IEnumerator stopRadio()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("Radio", false);
        sP.CallDrop();
        mN.money = mN.money -= sP.price;
        mN.updateMoney();
        canShoot = true;
        canRadio = true;
    }

    IEnumerator disableMuz()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        anim.SetBool("Shoot", false);
        canShoot = true;
    }

    IEnumerator disableMuzFR()
    {
        yield return new WaitForSeconds(.1f);
        muzzleFlash.SetActive(false);
    }

    public IEnumerator radioNoise()
    {
        yield return new WaitForSeconds(2f);
        aS.PlayOneShot(radioSound);
    }
}
