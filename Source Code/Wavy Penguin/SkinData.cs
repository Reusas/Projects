using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkinData
{
    public bool unlockedSkins;
    public bool unlockedSkins2;
    public bool unlockedSkins3;
    public bool unlockedSkins4;

    public SkinData(SkinSelector SS)
    {
        unlockedSkins = SS.unlockedSkins[1];
        unlockedSkins2 = SS.unlockedSkins[2];
        unlockedSkins3 = SS.unlockedSkins[3];
        unlockedSkins4 = SS.unlockedSkins[4];
    }


}
