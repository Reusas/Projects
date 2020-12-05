using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyBox : MonoBehaviour
{
    BoxCollider2D bC;
    Rigidbody2D rB;
    SpriteRenderer sR;
    Transform weaponHolderPlayer;
    Transform weaponHolderEnemy;
    BoxSpawner bS;

    Text wepText;

    public Sprite boxWithoutPara;

    public GameObject[] weapons;

    public int specificWeaponInd = -1;
    



    void Start()
    {
        wepText = GameObject.Find("WeaponBoxText").GetComponent<Text>();
        sR = GetComponent<SpriteRenderer>();
        bC = GetComponent<BoxCollider2D>();
        rB = GetComponent<Rigidbody2D>();
        bS = GameObject.FindWithTag("BoxL").GetComponent<BoxSpawner>();
        weaponHolderPlayer = GameObject.FindGameObjectWithTag("WeaponHolderPlayer").GetComponent<Transform>();
        weaponHolderEnemy = GameObject.FindGameObjectWithTag("WeaponHolderEnemy").GetComponent<Transform>();


        Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
        enem.goToPackage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            bC.isTrigger = false;
            rB.constraints = RigidbodyConstraints2D.FreezeAll;
            sR.sprite = boxWithoutPara;
        }
        else if (collision.transform.tag == "Player")
        {
            giveWeapons(0);

            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(gameObject);
            bS.spawnTheBox();
        }

        else if (collision.transform.tag == "Enemy")
        {
            giveWeapons(1);

            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(gameObject);
            bS.spawnTheBox();
        }
        
    }


    void giveWeapons(int i)
    {
        if (i == 0)
        {
            Destroy(weaponHolderPlayer.GetChild(0).gameObject);
            if (specificWeaponInd != -1)
            {
                Instantiate(weapons[specificWeaponInd], weaponHolderPlayer);
            }
            else
            {
                int x = Random.Range(0, weapons.Length);
                //weapons[x].GetComponent<Weapon>().user = Weapon.User.Player;
                Instantiate(weapons[x], weaponHolderPlayer);
                Weapon w = weapons[x].GetComponent<Weapon>();
                wepText.text = w.WeaponName;
            }
        }
        else
        {
            Destroy(weaponHolderEnemy.GetChild(0).gameObject);
            int x = Random.Range(0, weapons.Length);
           // weapons[x].GetComponent<Weapon>().user = Weapon.User.AI;
           GameObject wep= Instantiate(weapons[x], weaponHolderEnemy);
            Weapon w = wep.GetComponent<Weapon>();
            wepText.text = w.WeaponName;
            w.user = Weapon.User.AI;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            giveWeapons(0);

            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(gameObject);
            bS.spawnTheBox();
        }
        else if (collision.transform.tag == "Enemy")
        {
            giveWeapons(1);

            wepText.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
            int z = Random.Range(-45, 45);
            wepText.rectTransform.localEulerAngles = new Vector3(0, 0, z);
            wepText.GetComponent<WeaponText>().lerp = true;

            Enemy enem = GameObject.Find("Enemy").GetComponent<Enemy>();
            enem.goToPackage = false;

            Destroy(gameObject);
            bS.spawnTheBox();
        }
    }



}
