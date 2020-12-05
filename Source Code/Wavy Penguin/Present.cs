using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Present : MonoBehaviour
{
    Animator anim;
    public Image skinImg;
    public Sprite[] skins;

    public SkinSelector SS;
    public Player player;
    public Text coinText;
    public GameManager gM;

    bool canOpen=true;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void openPresent()
    {
        if (canOpen == true&&player.Coins>=10)
        {
            gM.canGoBack = false;
            player.Coins -= 10;
            player.SaveCoins();
            coinText.text = player.Coins.ToString();
            canOpen = false;
            anim.SetBool("Open", true);
            StartCoroutine(resetOpen());
            int x = Random.Range(0, 4);
            if (x == 0)
            {
                skinImg.sprite = skins[0];
                SS.unlockedSkins[1] = true;
                SS.SaveSkin();
            }
            else if (x == 1)
            {
                skinImg.sprite = skins[1];
                SS.unlockedSkins[2] = true;
                SS.SaveSkin();
            }
            else if (x == 2)
            {
                skinImg.sprite = skins[2];
                SS.unlockedSkins[3] = true;
                SS.SaveSkin();
            }
            else if (x == 3)
            {
                skinImg.sprite = skins[3];
                SS.unlockedSkins[4] = true;
                SS.SaveSkin();
            }
        }
    }


    IEnumerator resetOpen()
    {
        yield return new WaitForSeconds(3f);
        canOpen = true;
        anim.SetBool("Open", false);
        gM.canGoBack = true;

    }





}
