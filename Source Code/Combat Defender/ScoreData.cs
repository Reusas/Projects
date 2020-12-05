using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScoreData
{
    public int highScore;
    public ScoreData(Score sc)
    {
        highScore = sc.highScore;
    }

}
