using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[Serializable]
public class Player : MonoBehaviour
{
    #region Movement Variables
    public Transform playerAnchor;
    public Transform LeftTilt = null;
    public Transform RightTilt = null;
    public Camera playerCamera = null;
    public float limitY = 70.0f;
    public float cameraSmooth = 30.0f;
    public float tiltSmooth = 0.2f;
    public float speed = 10.0f;
    private float lookVertical = 0.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Transform RightHand = null;

    CharacterController characterController;
    Animator animator;
    private Vector2 moveAxis = Vector2.zero;
    #endregion
    #region Save System Variables
    public bool save = false;
    public bool load = false;
    public string saveName = "player";

    public int score = 10;
    public bool sad = false;
    public Vector3 penut = new Vector3(0, 22, 0.2f);
    #endregion
    #region Movement Functions
    public void ToggleInteract()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.Interact();
                    //Debug.Log("Intracted with " + interactable.name);
                }
            }
        }
    }

    public void HandleMovement()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ToggleInteract();
        }

        RightHand.Rotate(Input.mouseScrollDelta * 2.0f, Space.World);

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        moveDirection.y -= 20.0f * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        characterController.Move(moveDirection * Time.deltaTime);

        lookVertical += Input.GetAxisRaw("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * cameraSmooth * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
        {
            playerAnchor.localRotation = Quaternion.Lerp(RightTilt.rotation, playerAnchor.rotation, Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.Q))
        {
            playerAnchor.localRotation = Quaternion.Lerp(LeftTilt.rotation, playerAnchor.rotation, Time.deltaTime);

        }
        else
        {
            playerAnchor.localRotation = Quaternion.Lerp(Quaternion.identity, playerAnchor.localRotation, Time.deltaTime);
        }

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

    #endregion
    #region Save System Functions
    public void Load()
    {
        List<object> loadData = SaveSystem.Load<List<object>>(saveName);
        if (loadData != null)
        {
            SaveSystemSerialization.DeSerilizeTransform(loadData[0], transform);
            score = (int)loadData[1];
            sad = (bool)loadData[2];
            penut = SaveSystemSerialization.DeSerilizeVector3(loadData[3]);
        }
    }

    public void Save()
    {
        List<object> saveData = new List<object>();
        saveData.Add(SaveSystemSerialization.SerilizeTransform(transform));
        saveData.Add(score);
        saveData.Add(sad);
        saveData.Add(SaveSystemSerialization.SerilizeVector3(penut));
        SaveSystem.Save(saveName, saveData);
    }

    #endregion

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SaveSystem.LoadLog();
        SaveSystem.Destory(saveName);
    }

    public void Update()
    {
        if(save)
        {
            save = false;
            Save();
        }
        if(load)
        {
            load = false;
            Load();
        }

        //---Movement
        HandleMovement();
    }
}
