using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    public TMP_Text playerTeamTxt;

    public GameObject rightArrow;
    public GameObject leftArrow;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Image playerTeam;
    public Sprite[] teams;

    Player player;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerUi(player);
        UpdateTeamText(player);
    }

    public void ApplyLocalChanges()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);
    }

    public void LeftArrowClick()
    {
        if((int)playerProperties["playerTeam"] == 0)
        {
            playerProperties["playerTeam"] = teams.Length - 1;
        }
        else
        {
            playerProperties["playerTeam"] = (int)playerProperties["playerTeam"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void RightArrowClick()
    {
        if ((int)playerProperties["playerTeam"] == teams.Length - 1)
        {
            playerProperties["playerTeam"] = 0;
        }
        else
        {
            playerProperties["playerTeam"] = (int)playerProperties["playerTeam"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(player == targetPlayer)
        {
            UpdatePlayerUi(targetPlayer);
            UpdateTeamText(targetPlayer);
        }
    }

    public void UpdatePlayerUi(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerTeam"))
        {
            playerTeam.sprite = teams[(int)player.CustomProperties["playerTeam"]];
            playerProperties["playerTeam"] = (int)player.CustomProperties["playerTeam"];
        }
        else
        {
            playerProperties["playerTeam"] = 0;
        }
    }

    public void UpdateTeamText(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerTeam"))
        {
            if ((int)player.CustomProperties["playerTeam"] == 0)
            {
                playerTeamTxt.text = "Blue Team";
            }
            else if ((int)player.CustomProperties["playerTeam"] == 1)
            {
                playerTeamTxt.text = "Red Team";
            }
        }
    }
}
