using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private float player_1_RandomX;
    [SerializeField]
    private float player_2_RandomX;
    [SerializeField]
    private float player_1_randomZ;
    [SerializeField]
    private float player_2_randomZ;

    public GameObject platform1Prefab;
    public GameObject platform2Prefab;

    public GameObject spawnedPlatform1;
    public GameObject spawnedPlatform2;

    private void Start()
    {
        SpawnPlatform();
    }

    public void SpawnPlatform()
    {
        player_1_RandomX = Random.Range(-6, -1.2f);
        player_2_RandomX = Random.Range(6, 1.2f);
        player_1_randomZ = Random.Range(3.7f, -3.7f);
        player_2_randomZ = Random.Range(3.7f, -3.7f);

        spawnedPlatform1 = PhotonNetwork.InstantiateRoomObject(platform1Prefab.name, new Vector3(player_1_RandomX, 0.01f, player_1_randomZ), Quaternion.identity);
        spawnedPlatform2 = PhotonNetwork.InstantiateRoomObject(platform2Prefab.name, new Vector3(player_2_RandomX, 0.01f, player_2_randomZ), Quaternion.identity);
    }

    public void DestroyPlatform()
    {
        PhotonNetwork.Destroy(spawnedPlatform1);
        PhotonNetwork.Destroy(spawnedPlatform2);
    }
}
