using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{
    Vector3 playerPosition;     //Keeps track of where player will be teleported

    [SerializeField] float playerHeight = 1.8f;

    private void MoveTo(Transform destTransform)
    {
        //Set the new position
        playerPosition = destTransform.position;
        //Players eye level should be playerheight above the new position
        playerPosition.y += playerHeight;
        //Move player
        transform.position = playerPosition;
    }

    private void OnEnable()
    {
        TeleportManager.DoTeleport += MoveTo;
    }

    private void OnDisable()
    {
        TeleportManager.DoTeleport -= MoveTo;
    }
}
