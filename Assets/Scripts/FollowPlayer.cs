using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//try to center the player in the camera view and follow him when he move around the map (the camera will follow him) 

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}

