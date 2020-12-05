using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemCoins
{

    public static void SavePlayer(Player pl)
    {
        BinaryFormatter bF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/coins.wave";
        FileStream fS = new FileStream(path, FileMode.Create);
        CoinData sD = new CoinData(pl);
        bF.Serialize(fS, sD);
    }

    public static CoinData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/coins.wave";

        BinaryFormatter bF = new BinaryFormatter();
        FileStream fS = new FileStream(path, FileMode.Open);
        CoinData sD = bF.Deserialize(fS) as CoinData;
        fS.Close();
        return sD;



    }




}