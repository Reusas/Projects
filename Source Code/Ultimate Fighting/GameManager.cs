using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] player1Chars;
    public GameObject[] player2Chars;
    public Transform p1Spawn;
    public Transform p2Spawn;
    public SelectCharacter sC;
    public PlayerSpawnIndex pSI;
    public Sprite[] backGrounds;
    public GameObject BackGround;

    public PlayerController[] pCs;
    public Text winText;

    public GameObject restOfRing;

    Vector3 streetSize = new Vector3(3.642637f, 3.307734f, 1f);
    Vector3 streetPos = new Vector3(0.059f, -2.43f, 0f);

    Vector3 barSize=new Vector3(1.82391f, 1.690442f, 1);
    Vector3 barPos=new Vector3(-0.092f, -3.167f, 0);

    Vector3 ringSize = new Vector3(3.011853f, 1.690442f, 1);
    Vector3 ringPos = new Vector3(0.059f, -2.69f, 0);

    Vector3 trainSize = new Vector3(2.78699f, 3.121566f, 1);
    Vector3 trainPos = new Vector3(0.048f, -0.558f, 0);



    private void Start()
    {
        Instantiate(player1Chars[PlayerSpawnIndex.p1Index], p1Spawn.transform.position, p1Spawn.transform.rotation);
        Instantiate(player2Chars[PlayerSpawnIndex.p2Index], p2Spawn.transform.position, p2Spawn.transform.rotation);
        StartCoroutine(getPcs());

        if (PlayerSpawnIndex.mapIndex == 0)
        {
            BackGround.transform.localScale = streetSize;
            BackGround.transform.position = streetPos;
            BackGround.GetComponent<SpriteRenderer>().sprite = backGrounds[0];
            restOfRing.SetActive(false);
        }

        if (PlayerSpawnIndex.mapIndex == 1)
        {
            BackGround.transform.localScale = barSize;
            BackGround.transform.position = barPos;
            BackGround.GetComponent<SpriteRenderer>().sprite = backGrounds[1];
            restOfRing.SetActive(false);
        }

        if (PlayerSpawnIndex.mapIndex == 2)
        {
            BackGround.transform.localScale = ringSize;
            BackGround.transform.position = ringPos;
            BackGround.GetComponent<SpriteRenderer>().sprite = backGrounds[2];
            restOfRing.SetActive(true);
        }

        if (PlayerSpawnIndex.mapIndex == 3)
        {
            BackGround.transform.localScale = trainSize;
            BackGround.transform.position = trainPos;
            BackGround.GetComponent<SpriteRenderer>().sprite = backGrounds[3];
            restOfRing.SetActive(false);
        }

    }

    public void DisplayWinner(int i)
    {
        if (i == 0)
        {
            winText.text = "Player 1 Wins!!!";
            winText.gameObject.SetActive(true);
        }
        else if (i == 1)
        {
            winText.text = "Player 2 Wins!!!";
            winText.gameObject.SetActive(true);
        }
    }

    void selectMap()
    {

    }

    IEnumerator getPcs()
    {
        yield return new WaitForSeconds(.3f);
        pCs[0] = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pCs[1] = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);

    }

    public void shutItDown(int l)
    {
        if (l == 0)
        {
            pCs[0].enabled = false;

        }
        else if (l == 1)
        {
            pCs[1].enabled = false;
        }
        StartCoroutine(reset());
    }
}
