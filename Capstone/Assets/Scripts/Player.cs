using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class Player : MonoBehaviour
{
    public Camera playerCamera = null;
    public float limitY = 70.0f;
    public float cameraSmooth = 30.0f;
    public float speed = 10.0f;
    private float lookVertical = 0.0f;
    private Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;
    private Vector2 moveAxis = Vector2.zero;


    public void HandleMovement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        moveDirection.y -= 20.0f * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        characterController.Move(moveDirection * Time.deltaTime);

        lookVertical += Input.GetAxisRaw("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * cameraSmooth * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
            body.velocity += characterController.velocity;
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        //---Movement
        HandleMovement();
    }
}
