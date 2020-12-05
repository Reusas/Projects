using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCase : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int x = Random.Range(-75, 75);


        rb.AddForce(new Vector2(x, x));

        Destroy(gameObject, 3f);
    }


}
