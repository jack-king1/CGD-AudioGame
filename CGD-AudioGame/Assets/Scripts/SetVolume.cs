using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public float volume = 100.0f;
    string masterBusString = "Bus:/";
    FMOD.Studio.Bus masterBus;
    
    // Start is called before the first frame update
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
