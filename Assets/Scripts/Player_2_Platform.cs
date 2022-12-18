using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2_Platform : MonoBehaviour
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
        if ((int)PhotonNetwork.PlayerList[0].CustomProperties["playerTeam"] == 0)
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
        if (other.tag == "ball")
        {
            UiManager.UpdateScore(0);
            gameplayManager.ScoredGoal();
        }
    }
}
