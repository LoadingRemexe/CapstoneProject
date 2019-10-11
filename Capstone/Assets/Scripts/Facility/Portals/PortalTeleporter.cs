using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform reciever;
    [SerializeField] bool portalFlipped = true;

    private bool playerIsOverlapping = false;
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);

                if (portalFlipped)
                {
                    rotationDiff += 180;
                    player.Rotate(Vector3.up, rotationDiff);
                    Vector3 posOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                    player.position = reciever.position + posOffset;


                } else
                {
                    player.Rotate(Vector3.up, rotationDiff);
                    Vector3 posOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                    player.position = reciever.position + posOffset;
                    player.transform.rotation = Quaternion.AngleAxis(180, player.transform.up);

                }
            }
            playerIsOverlapping = false;

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
