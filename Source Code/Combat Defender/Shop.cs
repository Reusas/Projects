using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public Button SpeedButton;
    public Text speedText;
    public Button DamageButton;
    public Text damageText;
    public Button HeliButton;
    public Text heliText;
    public Button HealButton;
    PlayerController pC;
    Helicopter heli;
    Money mN;

    public bool shopOpen = false;

    public int[] speedPrice;
    public int[] damagePrice;
    public int[] heliPrice;
    public int healHeliPrice = 1000;
    Transform[] speedIm=new Transform[4];
    Transform[] damageIm = new Transform[4];
    Transform[] heliIm = new Transform[4];

    int speedUpgrade = 0;
    int damageUpgrade = 0;
    int heliUpgrade = 0;



    void Start()
    {
        pC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        mN = GameObject.Find("Money").GetComponent<Money>();
        heli = GameObject.Find("Heli").GetComponent<Helicopter>();
        speedText.text = "Increase player movement speed by 20% Price: " + speedPrice[0].ToString();
        damageText.text = "Increase player damage by 20% Price: " + speedPrice[0].ToString();
        heliText.text = "Increase helicopter health by 50% Price: " + heliPrice[0].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)){
            shopOpen = !shopOpen;
            if (shopOpen == true)
            {
                shop.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                shop.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }



    public void upgradeSpeed()
    {
        for (int i = 0; i < speedIm.Length; i++)
        {
            speedIm[i]=SpeedButton.transform.GetChild(i);
        }
        if (speedUpgrade == 0)
        {
            if (mN.money >= speedPrice[0])
            {
                mN.money -= speedPrice[0];
                speedUpgrade = 1;
                mN.updateMoney();
                speedIm[1].transform.GetComponent<Image>().color = Color.green;
                pC.speed += 100;
                speedText.text = "Increase player movement speed by 20% Price: " + speedPrice[1].ToString();
            }
        }

        else if (speedUpgrade == 1)
        {
            if (mN.money >= speedPrice[1])
            {
                mN.money -= speedPrice[1];
                speedUpgrade = 2;
                mN.updateMoney();
                speedIm[2].transform.GetComponent<Image>().color = Color.green;
                pC.speed += 100;
                speedText.text = "Increase player movement speed by 25% Price: " + speedPrice[2].ToString();
            }
        }

        else if (speedUpgrade == 2)
        {
            if (mN.money >= speedPrice[2])
            {
                mN.money -= speedPrice[2];
                speedUpgrade = 3;
                mN.updateMoney();
                speedIm[3].transform.GetComponent<Image>().color = Color.green;
                pC.speed += 175;
                speedText.text = "Upgrades finished!";
            }
        }
    }




    public void upgradeDamage()
    {
        for (int i = 0; i < damageIm.Length; i++)
        {
            damageIm[i] = DamageButton.transform.GetChild(i);
        }
        if (damageUpgrade == 0)
        {
            if (mN.money >= damagePrice[0])
            {
                mN.money -= damagePrice[0];
                damageUpgrade = 1;
                mN.updateMoney();
                damageIm[1].transform.GetComponent<Image>().color = Color.green;
                pC.damageMultiplier = 1.2f;
                damageText.text = "Increase player damage by 20% Price: " + damagePrice[1].ToString();
            }
        }

        else if (damageUpgrade == 1)
        {
            if (mN.money >= damagePrice[1])
            {
                mN.money -= damagePrice[1];
                damageUpgrade = 2;
                mN.updateMoney();
                damageIm[2].transform.GetComponent<Image>().color = Color.green;
                pC.damageMultiplier = 1.6f;
                damageText.text = "Increase player damage by 25% Price: " + damagePrice[2].ToString();
            }
        }

        else if (damageUpgrade == 2)
        {
            if (mN.money >= damagePrice[2])
            {
                mN.money -= damagePrice[2];
                damageUpgrade = 3;
                mN.updateMoney();
                damageIm[3].transform.GetComponent<Image>().color = Color.green;
                pC.damageMultiplier = 2f;
                damageText.text = "Upgrades finished!";
            }
        }
    }


    public void upgradeHeli()
    {
        for (int i = 0; i < heliIm.Length; i++)
        {
            heliIm[i] = HeliButton.transform.GetChild(i);
        }
        if (heliUpgrade == 0)
        {
            if (mN.money >= heliPrice[0])
            {
                mN.money -=heliPrice[0];
                heliUpgrade = 1;
                mN.updateMoney();
                heliIm[1].transform.GetComponent<Image>().color = Color.green;
                heli.maxHealth = 2000;
                heli.healthSlider.maxValue = heli.maxHealth;
                heli.healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
                heliText.text = "Increase helicopter health by 50% Price: " + heliPrice[1].ToString();
            }
        }

        else if (heliUpgrade == 1)
        {
            if (mN.money >= heliPrice[1])
            {
                mN.money -= heliPrice[1];
                heliUpgrade = 2;
                mN.updateMoney();
                heliIm[2].transform.GetComponent<Image>().color = Color.green;
                heli.maxHealth = 3000;
                heli.healthSlider.maxValue = heli.maxHealth;
                heli.healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
                heliText.text = "Increase player damage by 50% Price: " + heliPrice[2].ToString();
            }
        }

        else if (heliUpgrade == 2)
        {
            if (mN.money >= heliPrice[2])
            {
                mN.money -= heliPrice[2];
                heliUpgrade = 3;
                mN.updateMoney();
                heliIm[3].transform.GetComponent<Image>().color = Color.green;
                heli.maxHealth = 4000;
                heli.healthSlider.maxValue = heli.maxHealth;
                heli.healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 800);
                heliText.text = "Upgrades finished!";
            }
        }
    }


    public void healHeli()
    {
        if (mN.money >= healHeliPrice&&heli.health<heli.maxHealth)
        {
            heli.health = heli.maxHealth;
            mN.money -= healHeliPrice;
            mN.updateMoney();
            heli.healthSlider.value = heli.health;
        }
    }

    public void dropWeapon()
    {
        if (mN.money >= 1000)
        {
            Weapon wep = GameObject.Find("WeaponHolder").GetComponentInChildren<Weapon>();
            if (wep.canRadio == true)
            {
                wep.canRadio = false;
                wep.canShoot = false;
                wep.anim.SetBool("Radio", true);
                StartCoroutine(wep.stopRadio());
                StartCoroutine(wep.radioNoise());
            }
        }


        
    }
}


