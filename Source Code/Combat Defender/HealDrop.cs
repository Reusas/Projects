using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrop : MonoBehaviour
{
    Helicopter heli;
    void Start()
    {
        heli = GameObject.Find("Heli").GetComponent<Helicopter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            heli.health = heli.maxHealth;
            heli.healthSlider.value = heli.health;
            Destroy(transform.gameObject);
        }
    }
}
