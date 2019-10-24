using TMPro;
using UnityEngine;

public class HandheldScanner : MonoBehaviour
{
    [SerializeField] Transform pointer;
    [SerializeField] AudioSource ActivatedSFX;
    [SerializeField] LineRenderer laserLineRenderer;
    [SerializeField] TextMeshProUGUI readOut;
    [SerializeField] GameObject canvas;
    [SerializeField] LayerMask entityLayer;
    Carryable c;
    PlayerMove p;


    GameObject entityScanning = null;
    public bool ViewLine = true;

    float laserMaxLength = 5f;
    Vector3 endPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Carryable>();
        p = FindObjectOfType<PlayerMove>();
        if (ActivatedSFX) ActivatedSFX.Stop();

        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
    }

    // Update is called once per frame
    void Update()
    {

        if (ViewLine)
        {
            DrawLaser(pointer.position, pointer.forward, laserMaxLength);
            laserLineRenderer.enabled = true;
        }
        else
        {
            laserLineRenderer.enabled = false;
        }

        if (c.isHeld)
        {
            ViewLine = true;
            canvas.SetActive(true);
        }
        else
        {
            ViewLine = false;
            canvas.SetActive(false);
            readOut.text = "Please Scan A Valid Entity";
        }
    }
    public void ActivateScan()
    {

        Ray ray = new Ray(pointer.position, pointer.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, laserMaxLength, entityLayer))
        {
            if (raycastHit.collider.gameObject.CompareTag("Entity"))
            {
                Debug.Log("Hit Entity");

                EntityBasic a = raycastHit.collider.GetComponentInParent<EntityBasic>();
                if (a)
                {
                    readOut.text = a.Statistics;
                }
            }
        }

    }
    void DrawLaser(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}


