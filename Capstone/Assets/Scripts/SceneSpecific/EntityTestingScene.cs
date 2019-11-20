using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTestingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ScreenCover>().Screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
