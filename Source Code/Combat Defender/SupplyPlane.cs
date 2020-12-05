using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPlane : MonoBehaviour
{
    Animator anim;
    AudioSource aS;

    public Transform dropPoint;

    public GameObject box;

    public int price = 1000;

    void Start()
    {
        anim = GetComponent<Animator>();
        aS = GetComponent<AudioSource>();
    }

    public void CallDrop()
    {
        anim.SetBool("Supply", true);
        aS.PlayOneShot(aS.clip);
    }

    public void DropSupply()
    {
        Instantiate(box, dropPoint.position, dropPoint.rotation);
    }

    void stopFly()
    {
        anim.SetBool("Supply", false);
    }
}
