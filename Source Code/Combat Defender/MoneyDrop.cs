using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    Money mN;
    int moneyAmount;
    void Start()
    {
        mN = GameObject.Find("Money").GetComponent<Money>();
        Destroy(transform.gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            moneyAmount = Random.Range((int)100, (int)1000);
            mN.money += moneyAmount;
            mN.updateMoney();
            Destroy(transform.gameObject);
        }
    }
}
