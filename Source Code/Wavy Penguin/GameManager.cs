using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player pl;
    public Spawn sp;
    public Spawn sp2;
    public Spawn sp3;

    public GameObject openScreen;
    public GameObject deathScreen;
    public GameObject storeScreen;

    public SkinSelector SS;

    public Text coinText;

    public bool canGoBack = true;


    private void Start()
    {
        SS.LoadSkin();
    }

    public void BeginGame()
    {
        pl.enabled = true;
        sp.enabled = true;
        sp2.enabled = true;
        sp3.enabled = true;
        openScreen.gameObject.SetActive(false);
    }

    public void DeathScreen()
    {
        deathScreen.gameObject.SetActive(true);
    }

    public void Store()
    {
        openScreen.gameObject.SetActive(false);
        storeScreen.gameObject.SetActive(true);
        pl.LoadCoins();
        coinText.text = pl.Coins.ToString();
        
    }


    public void goBack()
    {
        if (canGoBack == true)
        {
            openScreen.gameObject.SetActive(true);
            storeScreen.gameObject.SetActive(false);
            SS.LoadSkin();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
