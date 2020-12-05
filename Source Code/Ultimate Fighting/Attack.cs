using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PlayerController pC;
    GameManager gM;
    public FightingController fC;
    public float damage=10;

    public bool lowAttack = false;
    public int knockBackPower = 30000;
    public GameObject[] blood;
    int bloodChance;
    Vector3 offset;
    public bool dieNinja = false;

    private void Start()
    {
        offset = new Vector2(0.2f, 0.3f);
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void bloodEffect()
    {
        bloodChance = Random.Range(0, 101);
        if (bloodChance > 60)
        {
            int x = Random.Range(0, 2);
            Instantiate(blood[x], transform.position+offset, transform.rotation);
        }
    }




    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.transform.tag == "Player")
        {
            
            pC = col.GetComponent<PlayerController>();
            fC = col.GetComponentInChildren<FightingController>();
            if (pC.health > 0)
            {
                if (lowAttack == true && fC.lowBlocking == true)
                {
                    pC.health -= damage / 4;
                    pC.updateHealthSlider();
                }
                if (fC.lowBlocking == false && lowAttack == false)
                {
                    pC.knockBack(-knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();

                }
                if (fC.lowBlocking == true && lowAttack == false)
                {
                    pC.knockBack(-knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();
                }

                else if (lowAttack == true && fC.lowBlocking == false)
                {
                    pC.knockBack(-knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();
                }
                if (pC.health <= 0)
                {
                    if (dieNinja == false)
                    {
                        pC.Die(0);
                    }
                    if (dieNinja == true)
                    {
                        pC.Die(1);
                    }

                    gM.DisplayWinner(1);
                    gM.shutItDown(1);
                }
            }
            



        }
        if (col.transform.tag == "Player2")
        {
            pC = col.GetComponent<PlayerController>();
            fC = col.GetComponentInChildren<FightingController>();
            if (pC.health > 0)
            {
                if (lowAttack == true && fC.lowBlocking == true)
                {
                    pC.health -= damage / 4;
                    pC.updateHealthSlider();
                }
                if (fC.lowBlocking == false && lowAttack == false)
                {
                    pC.knockBack(knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();
                }
                if (fC.lowBlocking == true && lowAttack == false)
                {
                    pC.knockBack(knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();
                }

                else if (lowAttack == true && fC.lowBlocking == false)
                {
                    pC.knockBack(knockBackPower);
                    pC.health -= damage;
                    pC.stopSpecial();
                    bloodEffect();
                    pC.updateHealthSlider();
                }
                if (pC.health <= 0)
                {
                    if (dieNinja == false)
                    {
                        pC.Die(0);
                    }
                    if (dieNinja == true)
                    {
                        pC.Die(1);
                    }
                    gM.DisplayWinner(0);
                    gM.shutItDown(0);
                }
            }
           



        }
    }


}
