using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingController : MonoBehaviour
{
    public PlayerController pC;
    public BoxCollider2D bC;
    public bool lowBlocking = false;
    public bool highBlocking = false;

    public BoxCollider2D HighAttackCol;
    public BoxCollider2D LowAttackCol;


    private void Start()
    {
        HighAttackCol.gameObject.SetActive(false);
        LowAttackCol.gameObject.SetActive(false);
    }

    void Update()
    {
        fightControls();
        blockControls();
        

    }
    



   public void fightControls()
    {
        if (pC.player2 == false)
        {
            if (Input.GetKeyDown(KeyCode.F) && pC.canAttack == true)
            {
                pC.anim.SetBool("HighAttack", true);
                pC.canAttack = false;
                pC.canMove = false;

            }
            else if (Input.GetKeyDown(KeyCode.G) && pC.canAttack == true)
            {
                pC.anim.SetBool("LowAttack", true);
                pC.canAttack = false;
                pC.canMove = false;

            }

        }
        if (pC.player2 == true)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1) && pC.canAttack == true)
            {
                pC.anim.SetBool("HighAttack", true);
                pC.canAttack = false;
                pC.canMove = false;

            }
            else if (Input.GetKeyDown(KeyCode.Keypad2) && pC.canAttack == true)
            {
                pC.anim.SetBool("LowAttack", true);
                pC.canAttack = false;
                pC.canMove = false;

            }
        }

    }

    public void blockControls()
    {

        if (pC.player2 == false)
        {


            if (Input.GetKey(KeyCode.C) && lowBlocking == false)
            {
                highBlocking = true;
                pC.anim.SetBool("BlockHigh", true);
                bC.offset = new Vector2(-0.02346787f, -0.6540366f);
                bC.size = new Vector2(0.3599741f, 0.8667411f);
                pC.canMove = false;
                pC.canAttack = false;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                highBlocking = false;
                pC.anim.SetBool("BlockHigh", false);
                bC.offset = new Vector2(-0.02346787f, -0.266113f);
                bC.size = new Vector2(0.3599741f, 1.642588f);
                pC.canMove = true;
                pC.canAttack = true;
            }

             if (Input.GetKey(KeyCode.V)&&highBlocking==false)
            {
                pC.anim.SetBool("BlockLow", true);
                pC.canMove = false;
                pC.canAttack = false;
                lowBlocking = true;
            }
            else if (Input.GetKeyUp(KeyCode.V))
            {
                pC.anim.SetBool("BlockLow", false);
                pC.canMove = true;
                pC.canAttack = true;
                lowBlocking = false;
            }
        }

        if (pC.player2 == true)
        {
            if (Input.GetKey(KeyCode.Keypad4) && lowBlocking == false)
            {
                highBlocking = true;
                pC.anim.SetBool("BlockHigh", true);
                bC.offset = new Vector2(-0.02346787f, -0.6540366f);
                bC.size = new Vector2(0.3599741f, 0.8667411f);
                pC.canMove = false;
                pC.canAttack = false;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                highBlocking = false;
                pC.anim.SetBool("BlockHigh", false);
                bC.offset = new Vector2(-0.02346787f, -0.266113f);
                bC.size = new Vector2(0.3599741f, 1.642588f);
                pC.canMove = true;
                pC.canAttack = true;
            }

            if (Input.GetKey(KeyCode.Keypad5)&&highBlocking==false)
            {
                pC.anim.SetBool("BlockLow", true);
                pC.canMove = false;
                pC.canAttack = false;
                lowBlocking = true;
            }
            else if (Input.GetKeyUp(KeyCode.Keypad5))
            {
                pC.anim.SetBool("BlockLow", false);
                pC.canMove = true;
                pC.canAttack = true;
                lowBlocking = false;
            }
        }


    }

    void HighAttack()
    {
        HighAttackCol.gameObject.SetActive(true);
    }

    void LowAttack()
    {
        LowAttackCol.gameObject.SetActive(true);
    }


    public void resetAttack()
    {
        pC.anim.SetBool("HighAttack", false);
        pC.anim.SetBool("LowAttack", false);
        HighAttackCol.gameObject.SetActive(false);
        LowAttackCol.gameObject.SetActive(false);
        pC.canMove = true;
        StartCoroutine(pC.enableAttack());
    }
}
