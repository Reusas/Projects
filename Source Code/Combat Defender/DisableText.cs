using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableText : MonoBehaviour
{
    Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "";

    }

    public void showText(int time,string st)
    {
        txt.text = st;
        StartCoroutine(disableText(time));

    }

    IEnumerator disableText(int t)
    {
        yield return new WaitForSeconds(t);
        txt.text = "";
    }


}
