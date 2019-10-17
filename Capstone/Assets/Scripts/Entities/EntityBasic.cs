﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBasic : MonoBehaviour
{
    [SerializeField] public ContainmentRoom containmentRoom;
    [SerializeField] public string ContainmentScene;
    [SerializeField] public string EmptyScene;
    public float TimeInContainment = 0.0f;

    private void Update()
    {
        TimeInContainment += Time.deltaTime;
    }

}