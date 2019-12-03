﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCP : MonoBehaviour
{
    public Transform activeCP;
    public Transform currentCP()
    {
        return activeCP.transform;
    }

    public void SetPlayerAtCP()
    {
        gameObject.transform.position = activeCP.transform.position;
    }
}