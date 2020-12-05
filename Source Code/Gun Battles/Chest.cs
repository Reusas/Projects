using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Chest : MonoBehaviour {
    public GameObject[] weapons;
    public Transform[] spawnPoints;
    public GameObject player;
    public Vector3 offset;
    public Vector3 Rocketoffset;
    private Rigidbody2D rb;
    int whichWeapon;
    int whichPoint;
    private GameObject currentWeapon;
    public float chestTime;

    private void Start()
    {
        currentWeapon = Instantiate(weapons[0], player.transform.position+offset, player.transform.rotation, player.transform);
        rb = GetComponent<Rigidbody2D>();
        whichPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[whichPoint].position;
        rb.isKinematic = true;
        Image im = transform.GetComponent<Image>();
        im.enabled = false;
        StartCoroutine(waitForChest());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

            ResetChest();
            SelectGun();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col.tag == "Ground")
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void ResetChest()
    {
        whichPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[whichPoint].position;
        rb.isKinematic = true;
        Image im = transform.GetComponent<Image>();
        im.enabled = false;
        StartCoroutine(waitForChest());
    }

    public void SelectGun()
    {
        Destroy(currentWeapon);
        whichWeapon = Random.Range(0, weapons.Length);

        if(player.transform.rotation.y==0 && whichWeapon < 7)
        {
            GameObject gun = Instantiate(weapons[whichWeapon], player.transform.position + offset, player.transform.rotation, player.transform);
            
            currentWeapon = gun;
        }
         if(player.transform.rotation.y==1&&whichWeapon<7)
        {
            GameObject gun = Instantiate(weapons[whichWeapon], player.transform.position - offset, player.transform.rotation, player.transform);
            currentWeapon = gun;
        }

        if (player.transform.rotation.y == 0&&whichWeapon==7)
        {
            GameObject gun = Instantiate(weapons[whichWeapon], player.transform.position + Rocketoffset, player.transform.rotation, player.transform);
            currentWeapon = gun;
        }
        if (player.transform.rotation.y == 1&&whichWeapon==7)
        {
            GameObject gun = Instantiate(weapons[whichWeapon], player.transform.position + Rocketoffset, player.transform.rotation, player.transform);
            currentWeapon = gun;
        }


    }

    public void GiveDefaultGun()
    {
        Destroy(currentWeapon);
        
        if (player.transform.rotation.y == 0)
        {

            GameObject gun = Instantiate(weapons[0], player.transform.position + offset, player.transform.rotation, player.transform);
            currentWeapon = gun;

        }
        if (player.transform.rotation.y == 1 )
        {
            GameObject gun = Instantiate(weapons[0], player.transform.position - offset, player.transform.rotation, player.transform);
            currentWeapon = gun;


        }

    }





    IEnumerator waitForChest()
    {
        yield return new WaitForSeconds(chestTime);
        rb.isKinematic = false;
        Image im = transform.GetComponent<Image>();
        im.enabled = true;


    }






}
