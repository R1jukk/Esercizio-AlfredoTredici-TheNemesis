using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;

public class UiManager : MonoBehaviourPunCallbacks
{
    public int player_1_Score;
    public int player_2_Score;

    public PhotonView uiView;

    public TMP_Text score;

    public TMP_Text Player_1_Nickname;
    public TMP_Text Player_2_Nickname;

    private void Start()
    {
        Player_1_Nickname.text = PhotonNetwork.PlayerList[0].NickName;
        Player_2_Nickname.text = PhotonNetwork.PlayerList[1].NickName;
        ResetScore();
    }

    public void UpdateScore(int player)
    {
        AddScore(player);
        
    }

    public void AddScore(int player)
    {
        ExitGames.Client.Photon.Hashtable score = new ExitGames.Client.Photon.Hashtable();
        if (player == 0)
        {
            player_1_Score++;   
            score.Add("score", player_1_Score);
            PhotonNetwork.PlayerList[player].SetCustomProperties(score);
        }
        else if (player == 1)
        {
            player_2_Score++;
            score.Add("score", player_2_Score);
            PhotonNetwork.PlayerList[player].SetCustomProperties(score);
        }
        
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        UpdateUi();
    }

    public void ResetScore()
    {
        player_1_Score = 0;
        player_2_Score = 0;
        PhotonNetwork.PlayerList[0].SetScore(0);
        PhotonNetwork.PlayerList[1].SetScore(0);
    }

    public void UpdateUi()
    {
        score.text = $"{PhotonNetwork.PlayerList[0].GetScore()} : {PhotonNetwork.PlayerList[1].GetScore()}";
    }
}
