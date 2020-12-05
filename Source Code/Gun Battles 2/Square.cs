using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    Rigidbody2D rb;

    int x = 2;

    float r;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        r = Random.Range(0.5f, 1.5f);
    }

    
    void Update()
    {

        rb.velocity = new Vector2(x, 0);

        if (transform.position.x > 9.5f)
        {
            x =-2;
        }

        if (transform.position.x < -9.5f)
        {
            x = 2;
        }

        if (transform.position.y > 4.7f)
        {
            rb.gravityScale = 5;
        }
        if (transform.position.y < -5.2f)
        {
            rb.gravityScale = -5;
        }

        transform.Rotate(0, 0, r);


        
    }


}
