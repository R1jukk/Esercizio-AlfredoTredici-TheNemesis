using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6;
    public Rigidbody rb;
    public PhotonView view;

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Movement();
        }
    }

    public void Movement()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = dir * speed;
    }

    
    public void DestroyAvatar()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
