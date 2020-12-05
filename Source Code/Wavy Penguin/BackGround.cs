using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Rigidbody2D rb;
    Player pl;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

   
    void Update()
    {
        transform.position = new Vector3(pl.transform.position.x+5, 0, 0);
    }
}
