using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawnpoint;
    public GameObject[] PlayerPrefab;
    public Transform currentSpawnpoint;

    new List<GameObject> spawnedPlayers1 = new List<GameObject>();

    private void Start()
    {
        SpawnPlayersAvatar();
    }

    public void SpawnPlayersAvatar()
    {
        GameObject playerToSpawn = PlayerPrefab[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerTeam"]];
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnpoint[0].position, Quaternion.identity, 0);
            spawnedPlayers1.Add(player);
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            GameObject player = PhotonNetwork.Instantiate(playerToSpawn.name, spawnpoint[1].position, Quaternion.identity, 0);
            spawnedPlayers1.Add(player);
        }

    }

}
