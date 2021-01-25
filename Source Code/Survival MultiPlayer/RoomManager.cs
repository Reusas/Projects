using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public override void OnEnable()
    {
        base.OnEnable();

        SceneManager.sceneLoaded += onSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= onSceneLoaded;
    }


    public void onSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
           GameObject player= PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"),Vector3.zero,Quaternion.identity);


        

        }
    }


    





}
