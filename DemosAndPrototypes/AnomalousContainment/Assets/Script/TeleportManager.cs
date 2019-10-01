using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(AudioSource))]
public class TeleportManager : MonoBehaviour
{
    public static Action<Transform> DoTeleport;
    [SerializeField] VRInteractiveItem[] teleportLocations;
    [SerializeField] Transform reticleTransform;

    private AudioSource asource;
    void Teleport()
    {
        if(DoTeleport != null)
        {
            if(asource == null) asource = GetComponent<AudioSource>();
            asource.Play();
            DoTeleport(reticleTransform);
        }
        else
        {
            Debug.Log("DoTeleport event has no subscribers");
        }
    }

    private void OnEnable()
    {
        asource = GetComponent<AudioSource>();
        foreach (VRInteractiveItem location in teleportLocations)
        {
            location.OnClick += Teleport;
        }
    }

    private void OnDisable()
    {
        foreach(VRInteractiveItem location in teleportLocations)
        {
            location.OnClick -= Teleport;
        }
    }

}
