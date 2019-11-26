using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    private GameObject owner_obj;
    private FMOD.Studio.EventInstance event_intance;
    // Start is called before the first frame update
    void Start(GameObject owner, FMOD.Studio.EventInstance event_inst)
    {
        owner_obj = owner;
        event_intance = event_inst;
    }

    FMOD.Studio.EventInstance GetInstance(GameObject owner)
    {
        if (owner == owner_obj)
        {
            return event_intance;
        }
        else return new FMOD.Studio.EventInstance event_inst;    
    }
}
