using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaBearController : MonoBehaviour
{
    public List<GameObject> eyes = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach(GameObject g in eyes)
        {
           g.transform.LookAt(FindObjectOfType<OVRCameraRig>().transform.position);
        } 
    }
}
