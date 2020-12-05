using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image instructionScreen;
    public Button[] buttons;
    public Slider loading;
    public Text loadText;

    public void startGame()
    {
        StartCoroutine(startG());
    }
    

    IEnumerator startG()
    {
        AsyncOperation op=SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        loading.gameObject.SetActive(true);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            loading.value = progress;
            yield return null;
        }
        
    }

    public void instructions()
    {
        instructionScreen.gameObject.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void goBack()
    {
        instructionScreen.gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
