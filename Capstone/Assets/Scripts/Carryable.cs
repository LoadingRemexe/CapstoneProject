using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour
{
    public AudioSource collisionSFX = null;

    Transform OriginalParent;
    bool isHeld = false;
    Rigidbody rb;
    MeshCollider body;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<MeshCollider>();
        OriginalParent = transform.parent;
    }
    private void Update()
    {
        if (isHeld) transform.localPosition= Vector3.zero;
    }

    public void PickupObject()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (rb) rb.useGravity = false;
        body.enabled = false;
        isHeld = true;
    }

    public void DropHeldObject()
    {
        transform.parent = OriginalParent;
        if (rb) rb.useGravity = true;
        body.enabled = true;
        isHeld = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (collisionSFX)
        {
            collisionSFX.Play();
        }
    }
}
