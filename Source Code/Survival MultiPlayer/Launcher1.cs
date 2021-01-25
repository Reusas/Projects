using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher1 : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject button;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject howToScreen;
    bool enabledHowToScreen = false;


    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {

        button.SetActive(true);
        button2.SetActive(true);

    }

    public void startGame()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public void howToMenu()
    {
        enabledHowToScreen = !enabledHowToScreen;
        howToScreen.SetActive(enabledHowToScreen);
    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }

    


}
