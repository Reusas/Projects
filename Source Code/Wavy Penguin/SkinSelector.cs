using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public Button back;
    public Button forward;

    public Image skinImg;
    public Sprite[] skins;

    public GameObject player;
    SpriteRenderer playerSr;

    public static int skinIndex=0;
    public bool[] unlockedSkins;


    private void Start()
    {
        if (unlockedSkins[skinIndex] == true)
        {
            skinImg.sprite = skins[skinIndex];
            changeSkin(skinIndex);
        }
    }

    public void forwardPress()
    {
        skinIndex++;
        if (skinIndex > skins.Length-1)
        {
            skinIndex = 0;
        }

        skinImg.sprite = skins[skinIndex];
        checkSkins();
    }

    public void backPress()
    {
        skinIndex--;
        if (skinIndex < 0)
        {
            skinIndex = skins.Length-1;
        }
        skinImg.sprite = skins[skinIndex];
        checkSkins();
        

    }

    void changeSkin(int sI)
    {
        playerSr = player.GetComponent<SpriteRenderer>();
        playerSr.sprite = skins[sI];
    }

    void checkSkins()
    {
        if (skinIndex == 1&&unlockedSkins[1]==false)
        {
            skinImg.color = Color.black;
        }

        else if (skinIndex == 2 && unlockedSkins[2] == false)
        {
            skinImg.color = Color.black;
        }

        else if (skinIndex == 3 && unlockedSkins[3] == false)
        {
            skinImg.color = Color.black;
        }

        else if (skinIndex == 4 && unlockedSkins[4] == false)
        {
            skinImg.color = Color.black;
        }

        else
        {
            skinImg.color = Color.white;
            changeSkin(skinIndex);
        }

    }

    public void SaveSkin()
    {
        SaveSystemSkins.SaveSkins(this);
        Debug.Log("w");
    }

    public void LoadSkin()
    {
        SkinData sD = SaveSystemSkins.LoadSkins();
        unlockedSkins[1] = sD.unlockedSkins;
        unlockedSkins[2] = sD.unlockedSkins2;
        unlockedSkins[3] = sD.unlockedSkins3;
        unlockedSkins[4] = sD.unlockedSkins4;
    }

}
