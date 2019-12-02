using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCP : MonoBehaviour
{
    public GameObject activeCP;
    public Transform currentCP()
    {
        return activeCP.transform;
    }
}
