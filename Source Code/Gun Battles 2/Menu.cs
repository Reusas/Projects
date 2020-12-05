using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject secondScreen;
    public Toggle AI, secondP;


    public void openSecondScreen()
    {
        secondScreen.SetActive(true);
    }

    public void loadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void quit()
    {
        Application.Quit();
    }
}
