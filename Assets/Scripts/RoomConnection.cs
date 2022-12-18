using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Cockpit;

public class RoomConnection : MonoBehaviourPunCallbacks
{
    public InputField roomName;
    public GameObject menuPanel;
    public GameObject submenupanel;
    public GameObject teamSelectionPanel;

    public List<PlayerItem> playerUiList = new List<PlayerItem>();
    public PlayerItem playerUIPrefab;
    public Transform playerUiParent;

    public GameObject playButton;

    
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom(null, 0, MatchmakingMode.FillRoom, null, null, null, new RoomOptions() { MaxPlayers = 2, BroadcastPropsChangeToAll = true});
    }

    public void CreateOrJoinButton()
    {
        menuPanel.SetActive(false);
        submenupanel.SetActive(true);
    }

    public void BackButton()
    {
        menuPanel.SetActive(true);
        submenupanel.SetActive(false);
    }

    public void CreateRoom()
    {
        if(roomName.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { MaxPlayers = 2, BroadcastPropsChangeToAll = true});
        }
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void LeveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ExitButton()
    {
        PhotonNetwork.Disconnect();
    }

    public void StarGame()
    {
        PhotonNetwork.LoadLevel("GameScene");
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }


    public override void OnJoinedRoom()
    {
        menuPanel.SetActive(false);
        submenupanel.SetActive(false);
        teamSelectionPanel.SetActive(true);
        UpdatePlayerUiList();
    }

    public override void OnLeftRoom()
    {
        menuPanel.SetActive(true);
        submenupanel.SetActive(false);
        teamSelectionPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerUiList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerUiList();
    }


    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.PlayerList[0].CustomProperties["playerTeam"].ToString() != PhotonNetwork.PlayerList[1].CustomProperties["playerTeam"].ToString())
        {
            playButton.SetActive(true);             
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void UpdatePlayerUiList()
    {
        foreach(PlayerItem UiItem in playerUiList)
        {
            Destroy(UiItem.gameObject);
        }
        playerUiList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem NewPlayerUi = Instantiate(playerUIPrefab, playerUiParent);
            NewPlayerUi.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                NewPlayerUi.ApplyLocalChanges();
            }
            playerUiList.Add(NewPlayerUi);
        }
    }
}
