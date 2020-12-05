using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemSkins 
{

    public static void SaveSkins(SkinSelector SS)
    {
        BinaryFormatter bF = new BinaryFormatter();
        string path = Application.persistentDataPath + "/skins.wave";
        FileStream fS = new FileStream(path, FileMode.Create);
        SkinData sD = new SkinData(SS);
        bF.Serialize(fS, sD);
    }

    public static SkinData LoadSkins()
    {
        string path = Application.persistentDataPath + "/skins.wave";
        BinaryFormatter bF = new BinaryFormatter();
        FileStream fS = new FileStream(path, FileMode.Open);
        SkinData sD = bF.Deserialize(fS) as SkinData;
        fS.Close();
        return sD;
    }



}
