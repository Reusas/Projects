using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] Soldier;
    public Text waveText;
    public Text enRemain;

    DisableText dT;

    public int waveLenght;
    public int wave;
    public int activeEnemies;
    public int spawnEnemies=3;
    public int timeBetweenWaves = 5;
    public int waveIncr = 2;

    public bool beginWave;
    public bool canSpawn = false;

    int x;
    int y;
    bool spawnAssault = false;
    bool spawnSniper = false;
    

    void Start()
    {
        waveText.text = "Wave: "+wave.ToString();
        dT = GameObject.Find("WeaponTextFam").GetComponent<Text>().GetComponent<DisableText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beginWave == true)
        {
            if (canSpawn == true)
            {
                StartCoroutine(spawnSmg(5, spawnEnemies));
                activeEnemies = spawnEnemies;
                canSpawn = false;
                updateRemainText();

            }



        }
    }


    public void updateRemainText()
    {
        enRemain.text = "Enemies remaining: " + activeEnemies.ToString();
    }

    public void checkWaveCompletion()
    {
        if (activeEnemies == 0)
        {
            StartCoroutine(startNextWave());
            dT.showText(2, "Wave complete! Next wave starting in 10 seconds.");
        }
    }



    IEnumerator spawnSmg(int delay,int length)
    {

        for (int i = 0; i < length; i++)
        {
            x = Random.Range(0, spawnPoints.Length);
            if (spawnAssault == false)
            {
                Instantiate(Soldier[0], spawnPoints[x].position, Soldier[0].transform.rotation);
            }
            else if (spawnAssault == true)
            {
                if (wave == 5)
                {
                  y = 1;
                }

                else if(wave>5&&wave<8)
                {
                    y = Random.Range(0, 2);
                }
                else if (wave == 8)
                {
                    y = 2;
                }
                else if (wave > 8&&wave<12)
                {
                    y = Random.Range(0, 3);
                }
                else if (wave == 12)
                {
                    y = 3;
                }

                else if (wave > 12 && wave < 15)
                {
                    y = Random.Range(0, 3);
                }
                else if (wave == 15)
                {
                    y = 4;
                }
                else
                {
                    y = Random.Range(0, 5);
                }

                if (y == 0)
                {
                    Instantiate(Soldier[0], spawnPoints[x].position, Soldier[0].transform.rotation);
                }
                if (y == 1)
                {
                    Instantiate(Soldier[1], spawnPoints[x].position, Soldier[1].transform.rotation);
                }

                if (y == 2)
                {
                    Instantiate(Soldier[2], spawnPoints[x].position, Soldier[2].transform.rotation);
                }
                if (y == 3)
                {
                    Instantiate(Soldier[3], spawnPoints[x].position, Soldier[3].transform.rotation);
                }

                if (y == 4)
                {
                    Instantiate(Soldier[4], spawnPoints[x].position, Soldier[4].transform.rotation);
                }


            }


            int del = Random.Range(0, delay);
            yield return new WaitForSeconds(del);
        }

    }

    IEnumerator startNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        spawnEnemies = spawnEnemies + waveIncr;
        canSpawn = true;
        wave++;
        waveText.text = "Wave: " + wave.ToString();
        if (wave == 5)
        {
            spawnEnemies = 3;
            spawnAssault = true;
            waveIncr = 3;
        }
        if (wave == 8)
        {
            spawnEnemies = 3;
            spawnSniper= true;
            waveIncr = 4;
        }
        if (wave == 12)
        {
            spawnEnemies = 3;
            spawnSniper = true;
            waveIncr = 5;
        }
        if (wave == 15)
        {
            spawnEnemies = 1;
            spawnSniper = true;
            waveIncr = 5;
        }

    }
}
