using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnIndex : MonoBehaviour
{
    public SelectCharacter sC;
    public bool player2 = false;
    public static int p1Index;
    public static int p2Index;
    public static int mapIndex;

    public Image mapIm;
    public Sprite[] images;



    public void map()
    {
        if (mapIndex < 3)
        {
            mapIndex++;
            mapIm.sprite = images[mapIndex];
        }
        else
        {
            mapIndex = 0;
            mapIm.sprite = images[mapIndex];
        }

    }

    public void saveIndex()
    {
        if (player2 == false)
        {
            p1Index = sC.characterIndex;
        }
        if (player2 == true)
        {
            p2Index = sC.characterIndex;
        }

    }
}
