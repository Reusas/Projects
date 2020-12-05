using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(Player pl)
    {
        BinaryFormatter bF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.wave";
        FileStream fS = new FileStream(path, FileMode.Create);
        ScoreData sD = new ScoreData(pl);
        bF.Serialize(fS, sD);
     }

    public static ScoreData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.wave";

            BinaryFormatter bF = new BinaryFormatter();
            FileStream fS = new FileStream(path, FileMode.Open);
            ScoreData sD=bF.Deserialize(fS) as ScoreData;
            fS.Close();
            return sD;

        
        
    }

   


}
