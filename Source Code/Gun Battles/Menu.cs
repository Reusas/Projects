using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public Button click;
    public Text btnText;
    public int bullets;
    public GameObject StudioIcon;
    public GameObject canvas;

	void Start () {
        StartCoroutine(begin());
        click = GetComponent<Button>();
        btnText = GetComponentInChildren<Text>();
        btnText.gameObject.SetActive(false);
        click.interactable = false;
        canvas.SetActive(false);



    }

    private void Update()
    {
        if (bullets == 6)
        {
            click.interactable = true;
            btnText.gameObject.SetActive(true);
        }
    }


    public void OnClick()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    IEnumerator begin()
    {
        yield return new WaitForSeconds(4);
        canvas.SetActive(true);
        Destroy(StudioIcon);
    }


}
