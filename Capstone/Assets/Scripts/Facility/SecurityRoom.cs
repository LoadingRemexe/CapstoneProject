using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityRoom : MonoBehaviour
{
    [SerializeField] CamMat[] cams;
    [SerializeField] Renderer MonitorScreen;

    int index = 0;
    float cameraTimer = 10f;

    [System.Serializable]
    struct CamMat
    {
       public Camera cam;
        public Material mat;
    }

    // Start is called before the first frame update
    void Start()
    {
        NextCamera();
    }

    // Update is called once per frame
    void Update()
    {
        cameraTimer -= Time.deltaTime;
        if (cameraTimer < 0.0f)
        {
            NextCamera();
        }
    }

    public void NextCamera()
    {
        cams[index].cam.gameObject.SetActive(false);
        index++;
        if (index >= cams.Length)
        {
            index = 0;
        }
        cameraTimer = 10.0f;
        cams[index].cam.gameObject.SetActive(true);
        MonitorScreen.material = cams[index].mat;
      //  Debug.Log("Showing" + cams[index].mat.name);

    }
}
