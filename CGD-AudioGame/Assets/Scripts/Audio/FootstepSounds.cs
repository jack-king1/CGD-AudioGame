using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    GameObject owner;
    private FMOD.Studio.EventInstance footstep_event;
    public FootstepSounds(GameObject obj, string sound)
    {
        owner = obj;
        footstep_event = FMODUnity.RuntimeManager.CreateInstance(sound);
    }

    public GameObject Owner() => owner;
    public FMOD.Studio.EventInstance GetEvent() => footstep_event;
}
