using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    public bool portalFlipped = true;

    private void Start()
    {
        playerCamera = FindObjectOfType<PlayerMove>().playerCamera.transform;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        if (portalFlipped)
        {
            transform.position = portal.position + playerOffsetFromPortal;
            float angDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion rotationalDifference = Quaternion.AngleAxis(angDifference, Vector3.up);
            Vector3 newCamDirection = rotationalDifference * playerCamera.forward;
            transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);

        }
        else
        {
            transform.position = portal.position - new Vector3(playerOffsetFromPortal.x, -playerOffsetFromPortal.y, playerOffsetFromPortal.z);
            float angDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion rotationalDifference = Quaternion.AngleAxis(angDifference, Vector3.up);
            Vector3 newCamDirection = rotationalDifference * playerCamera.forward;
            transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
        }

    }
}
