using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class PlayerAudioController : MonoBehaviour
{
    [FMODUnity.EventRef] public string footstep;
    [FMODUnity.EventRef] public string death;
    FMOD.Studio.EventInstance footstep_instance;
    FMOD.Studio.EventInstance death_instance;
    
    void Start()
    {
        death_instance = FMODUnity.RuntimeManager.CreateInstance(death);
        footstep_instance = FMODUnity.RuntimeManager.CreateInstance(footstep);
    }

    public void PlaySound(GameObject player, SOUND sound)
    {
        if (sound == SOUND.die)
        {
            death_instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player));
            death_instance.start();
        }
    }
}
