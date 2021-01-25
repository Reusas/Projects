using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class Respawner : MonoBehaviourPunCallbacks
{
    PhotonView pV;
    Spawner sP;
    Text text;
    int respawnTime = 10;

    public int playersAlive=0;
    bool LTaken = false;

    private void Awake()
    {
        pV = GetComponent<PhotonView>();
        text = GetComponent<Text>();
        sP = GameObject.Find("Spawner").GetComponent<Spawner>();


    }

    public void beginRespawn()
    {
        StartCoroutine(countDown());
        
    }


    IEnumerator countDown()
    {
        int resTime = 10;

        for (int i = 0; i < respawnTime; i++)
        {
            if (sP.playersAlive > 0)
            {
                text.text = "You died! Respawn in: " + resTime.ToString();
                yield return new WaitForSeconds(1f);
                resTime--;
            }
            else
            {
                LTaken = true;
                break;
            }


        }
        if (!LTaken)
        {
            text.text = "";
            respawnTime = 10;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
        }
        else
        {
            sP.gameOver();
            Debug.Log("LTAKEN0");
        }

    }




}
