using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class ScoreSaver
{
   public static void SaveScore(Score sc)
    {
        BinaryFormatter bF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.cd";
        FileStream fS = new FileStream(path, FileMode.Create);
        ScoreData sD = new ScoreData(sc);
        bF.Serialize(fS, sD);
    }

    public static ScoreData LoadScore()
    {
        string path = Application.persistentDataPath + "/score.cd";
        BinaryFormatter bF = new BinaryFormatter();
        FileStream fS = new FileStream(path, FileMode.Open);
        ScoreData sD = bF.Deserialize(fS) as ScoreData;
        fS.Close();
        return sD;

    }
}
