using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSounds : MonoBehaviour
{
    private GameObject owner;
    private FMOD.Studio.EventInstance hit_event;
    private FMOD.Studio.EventInstance loop_event;
    public ProjectileSounds(GameObject obj, string hit, string loop)
    {
        owner = obj;
        hit_event = FMODUnity.RuntimeManager.CreateInstance(hit);
        if (loop != "")
        {
            loop_event = FMODUnity.RuntimeManager.CreateInstance(loop);
        }
    }

    public GameObject Owner() => owner;
    public FMOD.Studio.EventInstance GetHit() => hit_event;
    public FMOD.Studio.EventInstance GetLoop() => loop_event;
}
