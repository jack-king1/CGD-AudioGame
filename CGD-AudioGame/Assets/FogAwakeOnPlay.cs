using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogAwakeOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fog;

    void Start()
    {
       fog.SetActive(true);
    }

}
