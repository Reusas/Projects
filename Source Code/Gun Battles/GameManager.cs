using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private Animator anim;
    public AudioSource ac;
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    

	void Start () {
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        anim = GetComponent<Animator>();
        ac = GetComponent<AudioSource>();
        ac.enabled = false;

    }


    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        GameObject GameMusic = GameObject.Find("Canvas");
        AudioSource gameA=GameMusic.GetComponent<AudioSource>();
        gameA.volume = 0;
        
    }

    public void Win()
    {
        WinScreen.SetActive(true);
        GameObject GameMusic = GameObject.Find("Canvas");
        AudioSource gameA = GameMusic.GetComponent<AudioSource>();
        gameA.volume = 0;
       
        if (LevelSelector.isPlayingCivilian == true && LevelSelector.levelComplete == false)
        {
            LevelSelector.isPlayingCivilian = false;
            LevelSelector.levelComplete = true;
        }
        if (LevelSelector.isPlayingGangster == true && LevelSelector.level2Complete == false)
        {
            LevelSelector.isPlayingGangster = false;
            LevelSelector.level2Complete = true;
        }
         if (LevelSelector.isPlayingSwat == true && LevelSelector.level3Complete == false)
        {
            LevelSelector.isPlayingSwat = false;
            LevelSelector.level3Complete = true;
        }
        if (LevelSelector.isPlayingSoldier == true && LevelSelector.level4Complete == false)
        {
            LevelSelector.isPlayingSoldier = false;
            LevelSelector.level4Complete = true;
        }
        if (LevelSelector.isPlayingSniper == true && LevelSelector.level5Complete == false)
        {
            LevelSelector.isPlayingSniper = false;
            LevelSelector.level5Complete = true;
        }
         if (LevelSelector.isPlayingBoss == true && LevelSelector.level6Complete == false)
        {
            LevelSelector.isPlayingBoss = false;
            LevelSelector.level6Complete = true;
        }



    }



    public void Menu()
    {

        SceneManager.LoadScene("LevelSelector");
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
