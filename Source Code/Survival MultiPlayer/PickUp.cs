using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum ItemType
    {
        Health,
        Damage,
        Speed,
    }

   public ItemType iT;


    void heal(PlayerController pC)
    {
        pC.health = 100;
        pC.takeDamage(0);
        
    }

    void damage(PlayerController pC)
    {
        pC.doubleDamage();
    }

    void speed(PlayerController pC)
    {
        pC.doubleSpeed();
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (iT == ItemType.Health)
        {
            heal(collision.transform.GetComponent<PlayerController>());
        }
        else if (iT == ItemType.Damage)
        {
            damage(collision.transform.GetComponent<PlayerController>());
        }
        else if (iT == ItemType.Speed)
        {
            speed(collision.transform.GetComponent<PlayerController>());
        }


        Destroy(gameObject);
    }
}
