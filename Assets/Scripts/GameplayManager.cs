using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    public GameObject spawnedBall;

    public GameObject winPanel;
    public GameObject losePanel;

    public PlayerSpawner playerSpawner;
    public PlayerMovement playerMovement;
    public UiManager uiManager;
    public GoalPlatformSpawner goalPlatformSpawner;

    
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        spawnedBall = PhotonNetwork.InstantiateRoomObject(ballPrefab.name, ballSpawnPoint.position, Quaternion.identity);  
    }

    private void Update()
    {

    }

    public void ScoredGoal()
    {
        CheckForWIn();

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(spawnedBall);
            goalPlatformSpawner.DestroyPlatform();
        }
        else
        {
            if(goalPlatformSpawner.spawnedPlatform1 != null && goalPlatformSpawner.spawnedPlatform2 != null)
            {
                goalPlatformSpawner.DestroyPlatform();
            }
        }
        spawnedBall = PhotonNetwork.InstantiateRoomObject(ballPrefab.name, ballSpawnPoint.position, Quaternion.identity);
        if(goalPlatformSpawner.spawnedPlatform1 != null && goalPlatformSpawner.spawnedPlatform2 != null)
        {
            goalPlatformSpawner.SpawnPlatform();
        }



        playerMovement.DestroyAvatar();

        playerSpawner.SpawnPlayersAvatar();
        playerMovement = FindObjectOfType<PlayerMovement>();


    }

    [PunRPC]
    public void CheckForWIn()
    {
        if (PhotonNetwork.PlayerList[0].GetScore() == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                winPanel.SetActive(true);
            }
            else
            {
                losePanel.SetActive(true);
            }
        }
        else if (PhotonNetwork.PlayerList[1].GetScore() == 2)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                losePanel.SetActive(true);
                
            }
            else
            {
                winPanel.SetActive(true);
            }
        }
    }

    public void NewGameButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("RoomSelectionScene");
    }

    public void ExitButton()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("MainMenu");   
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
    }
}
