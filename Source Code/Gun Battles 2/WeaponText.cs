using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponText : MonoBehaviour
{
    //Lerp stuff
    public bool lerp = false;
    int lerpValue = 2;
    float t = 0;
    float lerpSpeed = 1;

    Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
    }



    void Update()
    {
        if (lerp)
        {
            transform.localScale = new Vector3(Mathf.Lerp(1, lerpValue, t), Mathf.Lerp(1, lerpValue, t), 1);
            if (t < 1)
            {
                t += lerpSpeed * Time.deltaTime;
            }
            else
            {
                lerp = false;
                t = 0;
                txt.text = "";
            }
        }
    }
}
