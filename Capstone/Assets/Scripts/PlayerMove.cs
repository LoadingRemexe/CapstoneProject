using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Camera playerCamera = null;
    [SerializeField] Transform PlayerHand;

    float limitY = 60.0f;
    float cameraSmooth = 10.0f;
    float speed = 10.0f;
    public float SightDistance = 5.0f;

    float lookVertical = 0.0f;
    Carryable heldObject = null;

    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToggleInteract();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DropObject();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && heldObject)
        {
            heldObject.transform.Rotate(Vector3.one * 15f, Space.World);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && heldObject)
        {
            heldObject.transform.Rotate(Vector3.one * -15f, Space.World);
        }
    }

    public void ToggleInteract() 
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, SightDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
                //Debug.Log("Intracted with " + interactable.name);
            }
        }
    }

    public void PickUpObject(Carryable carryObj)
    {
        DropObject();
        heldObject = carryObj;
        PlayerHand.rotation = Quaternion.identity;
        carryObj.transform.parent = PlayerHand;
    }

    public void DropObject()
    {
        if (heldObject) heldObject.DropHeldObject();
        heldObject = null;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.MovePosition(rb.transform.position += (rb.transform.forward * Input.GetAxis("Vertical") + rb.transform.right * Input.GetAxis("Horizontal")) * speed * Time.deltaTime);

        lookVertical += Input.GetAxisRaw("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        rb.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraSmooth * Time.deltaTime);
    }
}