using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    Image im;
    public Image[] characterImages;
    public int characterIndex = 0;
    public SelectCharacter sC;
    public PlayerSpawnIndex pSI;

    public bool player2 = false;
    public bool player1Ready = false;
    public bool player2Ready = false;
    void Start()
    {
        im = GetComponent<Image>();
        im.transform.position = characterImages[characterIndex].transform.position;
    }

    void Update()
    {
        if (player2 == false)
        {
            if (Input.GetKeyDown(KeyCode.D) && characterIndex < characterImages.Length - 1)
            {
                characterIndex++;
            }
            else if (Input.GetKeyDown(KeyCode.A) && characterIndex > 0)
            {
                characterIndex--;
            }
            if (Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.K)|| Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I))
            {
                player1Ready = true;
                checkIfReady();
                pSI.saveIndex();             
                Destroy(gameObject);
            }
        }
        if (player2 == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && characterIndex < characterImages.Length - 1)
            {
                characterIndex++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && characterIndex > 0)
            {
                characterIndex--;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1)|| Input.GetKeyDown(KeyCode.Keypad2)|| Input.GetKeyDown(KeyCode.Keypad4)||Input.GetKeyDown(KeyCode.Keypad5))
            {
                player2Ready = true;
                checkIfReady();
                pSI.saveIndex();
                Destroy(gameObject);
            }
        }


        im.transform.position = characterImages[characterIndex].transform.position;

        

    }


    void checkIfReady()
    {
        if (player2 == false)
        {
            if (player1Ready == true && sC.player2Ready == true)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (player2 == true)
        {
            if (sC.player1Ready == true && player2Ready == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }
}
