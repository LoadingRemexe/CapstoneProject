using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    [SerializeField] AudioSource footsteps;

    public void PlayFootstep()
    {
        footsteps.Play();
    }
}
