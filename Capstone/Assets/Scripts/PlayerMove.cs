using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour, PlayerControls.IPlayerActions
{
    [SerializeField] public Camera playerCamera = null;
    [SerializeField] Transform PlayerHand;
    [SerializeField] LayerMask sightLayerMask;
    [SerializeField] Animator animator;
    [SerializeField] PlayerControls controls;


    float limitY = 60.0f;
    public float cameraSmooth { get; set; } = 30f;
    public float speed { get; set; } = 5f;
    public float SightDistance { get; set; } = 3.0f;

    float lookVertical = 0.0f;
    Vector2 Looking = Vector2.zero;
    Vector2 Movement = Vector2.zero;
    public Carryable heldObject { get; set; } = null;

    Rigidbody rb;
    public GameObject objectInSight= null;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    #region Input Controls
    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
    }
    void OnEnable()
    {
        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // Debug.Log("Move" + context.ReadValue<Vector2>());

        Movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Debug.Log("look" + context.ReadValue<Vector2>());
        Looking = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        ToggleInteract();
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (heldObject && heldObject.UsableObjectFunction != null)
        {
            heldObject.UsableObjectFunction.Invoke();
        }
    }
    public void OnPickUp(InputAction.CallbackContext context)
    {
        DropObject();
        TogglePickup();
    }
    public void OnScroll(InputAction.CallbackContext context)
    {
        float rotate = 0;
        if (context.ReadValue<float>() > 0)
        {
            rotate = 30.0f;
        }
        else if (context.ReadValue<float>() < 0)
        {
            rotate = -30.0f;
        }
        PlayerHand.transform.Rotate(Vector3.up, rotate * Time.deltaTime);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Cursor.lockState = (Time.timeScale == 1)? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
    }

    #endregion

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, SightDistance, sightLayerMask))
        {
            objectInSight = hit.collider.gameObject;
            HoverObject h = objectInSight.GetComponent<HoverObject>();
            if (h)
            {
                h.OnHover.SetActive(true);
            }
        }
        else
        {
            objectInSight = null;
        }

        animator.SetBool("HoldItem", heldObject != null);

    }

    public void ToggleInteract()
    {
        if (objectInSight)
        {
            Interactable interactable = objectInSight.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    public void TogglePickup()
    {
        if (objectInSight)
        {
            Carryable carry = objectInSight.GetComponent<Carryable>();
            if (carry != null)
            {
                carry.PickupObject();
                //Debug.Log("picked up" + carry.name);
            }
        }
    }

    public void PickUpObject(Carryable carryObj)
    {
        DropObject();

        heldObject = carryObj;
        PlayerHand.localRotation = Quaternion.identity;
        carryObj.transform.parent = PlayerHand;
    }

    public void DropObject()
    {
        if (heldObject) heldObject.DropHeldObject();
        heldObject = null;
    }

    private void FixedUpdate()
    {
        lookVertical += Looking.y * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        rb.transform.Rotate(Vector3.up, Looking.x * cameraSmooth * Time.deltaTime);

        rb.velocity = Vector3.zero;
        rb.MovePosition(rb.transform.position += ((rb.transform.forward * Movement.y + rb.transform.right * Movement.x) * speed * Time.deltaTime));
        animator.SetFloat("Speed", Movement.y + Movement.x);


    }

}