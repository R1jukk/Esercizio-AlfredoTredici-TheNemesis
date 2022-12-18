using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Network : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    public TMP_Text buttonText;

    public void Connect()
    {
        if(playerName.text.Length >= 1)
        {
            PhotonNetwork.NickName = playerName.text;
            buttonText.text = "Connecting..";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("RoomSelectionScene");
    }
}
