using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackLilBox : MonoBehaviour
{

    public PlayerController pC;
    public AudioSource aS;
    public Slider[] specialSliders;
    public Slider mainSlider;
    public BoxCollider2D bC;
    public Attack at;

    public float healNum = 10;
    public float specialInc = 5f;
    public float specialNeed = 100;

    private void Start()
    {
        specialSliders[0] = GameObject.Find("P1Special").GetComponent<Slider>();
        specialSliders[1] = GameObject.Find("P2Special").GetComponent<Slider>();
    }
    void updateSpecialSlider()
    {
        if (pC.special <= 100)
        {
            pC.special += specialInc * Time.deltaTime;
        }

        if (pC.player2 == false)
        {
            mainSlider = specialSliders[0];
            mainSlider.value = pC.special;
        }
        if (pC.player2 == true)
        {
            mainSlider = specialSliders[1];
            mainSlider.value = pC.special;
        }
    }

    private void Update()
    {
        updateSpecialSlider();

        if (Input.GetKeyDown(KeyCode.Space) && pC.canAttack == true && pC.player2 == false)
        {
            if (pC.special >= 100)
            {
                pC.anim.SetBool("Special", true);
                pC.special -= specialNeed;
            }

        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && pC.canAttack == true && pC.player2 == true)
        {
            if (pC.special >= 100)
            {
                pC.anim.SetBool("Special", true);
                pC.special -= specialNeed;
            }
        }
    }

    public void activatePunch()
    {
        at.damage = 5;
        aS.PlayOneShot(aS.clip);
        bC.gameObject.SetActive(true);
        pC.canAttack = false;
        pC.canMove = false;

    }

    public void stopPunch()
    {
        at.damage = 10;
        bC.gameObject.SetActive(false);
    }
    public void stopSpecial()
    {
        at.damage = 10;
        pC.anim.SetBool("Special", false);
        pC.canAttack = true;
        pC.canMove = true;
    }




}
