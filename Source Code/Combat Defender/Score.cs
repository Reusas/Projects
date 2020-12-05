using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public WaveSpawner wS;

    public Text scoreTxt;
    public Text highScoreTxt;

    public int score = 0;
    public int highScore = 0;


    public void updateScore()
    {
        score = wS.wave;
        scoreTxt.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            highScoreTxt.text = "Highscore: " + highScore;
            ScoreSaver.SaveScore(this);
        }
        highScoreTxt.text = "Highscore: " + highScore;
    }
    
}

