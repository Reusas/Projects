using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections;
using ExitGames.Client.Photon;

public class Spawner : MonoBehaviourPunCallbacks
{

    [SerializeField] Text enemiesAliveText;
    [SerializeField] Text gameOverText;
    [SerializeField] Transform[] spawnPoints;
    PhotonView pV;
    bool canSpawn = true;

   


    public int enemiesAlive;
    public int playersAlive;
    int enemiesThisWave = 5;

    bool waveBegun = false;


    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            canSpawn = false;
        }

        pV = GetComponent<PhotonView>();

        

    }

    private void Start()
    {

        StartCoroutine(spawnRoutine());
    }


    public void spawnEnemy(int i)
    {
        if (canSpawn)
        {

            string[] enemy = new string[] {"Enemy","Enemy2","Enemy3","Enemy4" };

            int x = Random.Range(0, spawnPoints.Length);

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", enemy[i]),spawnPoints[x].position,Quaternion.identity);
            enemiesAlive++;
            updateEnemies();

        }

    }

    IEnumerator spawnRoutine()
    {

        waveBegun = true;
        for (int i = 0; i < enemiesThisWave;i++)
        {
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);

            int chance = Random.Range(1, 101);
            if (chance <= 75)
            {
                spawnEnemy(0);
            }
            else if (chance > 75)
            {
                int c = Random.Range(1, 4);
                spawnEnemy(c);
            }

        }

        waveBegun = false;
    }

    public void updateEnemiesAliveText()
    {
        photonView.RPC("RPC_uEAT", RpcTarget.All);
    }

    [PunRPC]
    void RPC_uEAT()
    {
        enemiesAliveText.text = "Enemies :" + enemiesAlive.ToString();
    }



   public void updateEnemies()
    {
        Hashtable hash = new Hashtable();
        hash.Add("EnemiesAlive", enemiesAlive);

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);


        if (enemiesAlive <= 0 && waveBegun == false)
        {
            int x = Random.Range(1, 4);

            enemiesThisWave += x;

            StartCoroutine(spawnRoutine());
        }

        updateEnemiesAliveText();
        

    }


    public void addPlayer(string add)
    {
        if (add == "+")
        {
            playersAlive++;
        }
        else if (add == "-")
        {
            playersAlive--;
        }


        Hashtable hash2 = new Hashtable();
        hash2.Add("Players", playersAlive);


        PhotonNetwork.CurrentRoom.SetCustomProperties(hash2);





        
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {

         enemiesAlive = (int)changedProps["EnemiesAlive"];


        

        

    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {

       bool t= PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Players");

        if (t == true)
        {
            playersAlive = (int)propertiesThatChanged["Players"];
            Debug.Log("UpdatedPlayers");
        }

        
        

    }


    public void gameOver()
    {
        pV.RPC("GAMEOVER", RpcTarget.All);
    }


    [PunRPC]
    public void GAMEOVER()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(loadMenu());
    }

    IEnumerator loadMenu()
    {

        yield return new WaitForSeconds(3f);
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();

        PhotonNetwork.LoadLevel(0);
    }

}
