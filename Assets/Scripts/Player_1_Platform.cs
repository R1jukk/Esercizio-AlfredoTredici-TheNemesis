using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Player_1_Platform : MonoBehaviourPunCallbacks
{
    public Material platformMaterial;
    public Color blueTeamColor;
    public Color redTeamColor;
    public UiManager UiManager;

    public GameplayManager gameplayManager;

    private void Start()
    {
        UiManager = GameObject.Find("GameManager").GetComponent<UiManager>();
        gameplayManager = GameObject.Find("GameManager").GetComponent<GameplayManager>();
        CheckPlayerTeam();
    }

    public void CheckPlayerTeam()
    {
        if ((int)PhotonNetwork.PlayerList[1].CustomProperties["playerTeam"] == 0)
        {
            platformMaterial.color = blueTeamColor;
        }
        else
        {
            platformMaterial.color = redTeamColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
        {
            UiManager.UpdateScore(1);
            gameplayManager.ScoredGoal();
        }
        
    }
}
