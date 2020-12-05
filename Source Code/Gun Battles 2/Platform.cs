using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformEffector2D pef;
    Enemy eN;
    PlayerController pC;

    bool canDrop = false;

    bool canDropE = false;

    private void Start()
    {
        pef = GetComponent<PlatformEffector2D>();
        eN = GameObject.Find("Enemy").GetComponent<Enemy>();
        pC = GameObject.Find("Player").GetComponent<PlayerController>();
    }
 

    private void Update()
    {
        if (canDrop == true)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {

                StartCoroutine(dropPlayer());
            }
        }
        if (canDropE == true)
        {

            if (eN.transform.position.y > pC.transform.position.y)
            {

                StartCoroutine(dropEnemy());
            }

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canDrop = true;
        }
        if (collision.transform.tag == "Enemy")
        {
            canDropE = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            canDrop = false;
        }
        if (collision.transform.tag == "Enemy")
        {
            canDropE = false;
        }
        

    }

    IEnumerator dropPlayer()
    {

        pef.colliderMask = LayerMask.GetMask("Enemy");
        yield return new WaitForSeconds(.5f);
        pef.colliderMask = LayerMask.GetMask("Enemy", "Player");
    }

    IEnumerator dropEnemy()
    {
        yield return new WaitForSeconds(.5f);
        pef.colliderMask = LayerMask.GetMask("Player");
        yield return new WaitForSeconds(.5f);
        pef.colliderMask = LayerMask.GetMask("Enemy", "Player");
    }
}
