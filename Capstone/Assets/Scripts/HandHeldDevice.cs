using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeldDevice : MonoBehaviour
{
    [SerializeField] Transform pointer;
    [SerializeField] AudioSource ActivatedSFX;
    [SerializeField] LineRenderer laserLineRenderer;
    [SerializeField] LayerMask entityLayer;
    Carryable c;
    PlayerMove p;


    GameObject entityRetreiving = null;
    public bool ViewLine = true;

    float laserWidth = 0.1f;
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
        laserLineRenderer.widthMultiplier = laserWidth;
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

        if (entityRetreiving && c.isHeld)
        {
            entityRetreiving.transform.position = Vector3.Lerp(pointer.position + pointer.forward, entityRetreiving.transform.position, Time.deltaTime);
        } else if (!c.isHeld && entityRetreiving)
        {
            entityRetreiving.GetComponentInParent<Animator>().SetBool("Retreive", false);
            Debug.Log("DeActivate Retreive");
            entityRetreiving = null;
            if (ActivatedSFX) ActivatedSFX.Stop();
        }
    }

    public void ActivateRetreive()
    {
        Debug.Log("Activate Retreive");
        if (entityRetreiving == null)
        {
            Ray ray = new Ray(pointer.position, pointer.forward);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, laserMaxLength))
            {
                if (raycastHit.collider.gameObject.CompareTag("Entity"))
                {
                    Debug.Log("Hit Entity");

                    Animator a = raycastHit.collider.GetComponentInParent<Animator>();
                    if (a)
                    {
                        a.SetBool("Retreive", true);
                        entityRetreiving = raycastHit.collider.gameObject;
                        Debug.Log("Retreival Successful");
                        if (ActivatedSFX) ActivatedSFX.Play();

                    }
                }
            }
        } else
        {
            entityRetreiving.GetComponentInParent<Animator>().SetBool("Retreive", false);
            Debug.Log("DeActivate Retreive");
            entityRetreiving = null;
            if (ActivatedSFX) ActivatedSFX.Stop();

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


