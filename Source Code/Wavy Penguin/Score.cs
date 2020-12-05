using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public int score=0;
    Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
    }


    public void updateText()
    {
        score++;
        txt.text = score.ToString();
    }
}
