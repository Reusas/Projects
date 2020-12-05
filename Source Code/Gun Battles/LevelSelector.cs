using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
    public static int whichLevel;
    public static bool levelComplete = false;
    public static bool level2Complete = false;
    public static bool level3Complete = false;
    public static bool level4Complete = false;
    public static bool level5Complete = false;
    public static bool level6Complete = false;
    public static bool isPlayingCivilian = false;
    public static bool isPlayingGangster = false;
    public static bool isPlayingSwat = false;
    public static bool isPlayingSoldier = false;
    public static bool isPlayingSniper = false;
    public static bool isPlayingBoss = false;
    public GameObject[] levels;

    private void Start()
    {
        levels[0].SetActive(true);

        if (levelComplete)
        {
            levels[1].SetActive(true);
        }
        if (level2Complete)
        {
            levels[2].SetActive(true);
        }
        if (level3Complete)
        {
            levels[3].SetActive(true);
        }
        if (level4Complete)
        {
            levels[4].SetActive(true);
        }
        if (level5Complete)
        {
            levels[5].SetActive(true);
        }

    }



    public void loadCivilian()
    {
      
      SceneManager.LoadScene(2);
      isPlayingCivilian = true;

    }

    public void loadGangster()
    {

        SceneManager.LoadScene(3);
        isPlayingGangster = true;

    }

    public void loadSwat()
    {

        SceneManager.LoadScene(4);
        isPlayingSwat = true;

    }

    public void loadSoldier()
    {

        SceneManager.LoadScene(5);
        isPlayingSoldier = true;

    }
    public void loadSniper()
    {

        SceneManager.LoadScene(6);
        isPlayingSniper = true;

    }

    public void loadBoss()
    {

        SceneManager.LoadScene(7);
        isPlayingBoss = true;
        BulletCollision.isBoss = true;

    }
}
