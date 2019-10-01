using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour
{
    bool isHeld = false;
    private Transform playerHand;
    private Rigidbody rb;
    public AudioSource collisionSFX = null;


    void Start()
    {
        playerHand = FindObjectOfType<Player>().RightHand;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (isHeld)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        if (isHeld && Input.GetMouseButtonDown(1))
        {
            DropHeldObject();
        }
    }
    public void PickupObject()
    {
        playerHand.rotation = Quaternion.identity;

        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (rb)
        {
            rb.useGravity = false;
        }
        isHeld = true;
    }
    public void DropHeldObject()
    {
        transform.parent = null;
        isHeld = false;
        if (rb)
        {
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionSFX && other.tag == "World")
        {
            collisionSFX.Play();
        }
    }
}
