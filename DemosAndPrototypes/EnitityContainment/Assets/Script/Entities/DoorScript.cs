using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorScript : MonoBehaviour
{
    [SerializeField] string m_playerTag = "Player";
    [SerializeField] Animator m_animator = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.tag == m_playerTag)
        {
            m_animator.SetTrigger("Open");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == m_playerTag)
        {
            m_animator.SetTrigger("Close");
        }
    }
}
