using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    Text txt;

    public int money;

    void Start()
    {
        txt= GetComponent<Text>();
        txt.text = "$: " + money;
    }


    public void updateMoney()
    {
        txt.text = "$: " + money;
    }
}
