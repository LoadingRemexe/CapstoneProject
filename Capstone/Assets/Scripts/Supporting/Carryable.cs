using UnityEngine;
using UnityEngine.Events;

public class Carryable : MonoBehaviour
{
    [SerializeField] AudioSource collisionSFX = null;
    [SerializeField] public UnityEvent UsableObjectFunction = null;

    Transform OriginalParent;
    public bool isHeld = false;
    Rigidbody rb;
    MeshCollider body;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<MeshCollider>();
        OriginalParent = transform.parent;
    }
    private void Update()
    {
        if (isHeld)
        {
            transform.localPosition = Vector3.zero;
        }
    }
    //  Pickup by the player
    public void PickupObject()
    {
        FindObjectOfType<PlayerMove>().PickUpObject(this);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        isHeld = true;
        if (rb) rb.useGravity = false;
        body.enabled = false;
    }

    public void DropHeldObject()
    {
        isHeld = false;
        transform.parent = OriginalParent;
        transform.localScale = Vector3.one;
        if (rb) rb.useGravity = true;
        body.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionSFX.Play();
    }
}
