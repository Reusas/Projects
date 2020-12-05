using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject lossScreen;


    public void WinScreen()
    {
        winScreen.SetActive(true);
    }

    public void LossScreen()
    {
        lossScreen.SetActive(true);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
