

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera = null;
    public float limitY = 60.0f;
    public float cameraSmooth = 10.0f;

    public float speed = 10.0f;
    private float translation;
    private float straffe;
    private float lookVertical = 0.0f;

    Rigidbody rb;
    Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

      //  translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        //straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
       
        rb.transform.position += (rb.transform.forward * Input.GetAxis("Vertical") + rb.transform.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime;

        lookVertical += Input.GetAxisRaw("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        rb.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * cameraSmooth * Time.deltaTime);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        
    }
}