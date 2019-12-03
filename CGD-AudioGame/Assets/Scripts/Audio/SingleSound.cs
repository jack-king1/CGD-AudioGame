using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSound : MonoBehaviour
{
    GameObject owner;
    private FMOD.Studio.EventInstance sound_instance;
    private float vol_multi = 1;

    public SingleSound(GameObject obj, string sound)
    {
        owner = obj;
        sound_instance = FMODUnity.RuntimeManager.CreateInstance(sound);
    }

    public FMOD.Studio.EventInstance GetEvent() => sound_instance;
    public GameObject GetOwner() => owner;
    public float GetVolMultiplier() => vol_multi;
    public void SetVolMultiplier(float multi) => vol_multi = multi;
    public void SetVolume(float volume) => sound_instance.setParameterValue("Volume", volume * vol_multi);
    public GameObject Owner() => owner;

    public float GetVolume()
    {
        float vol;
        sound_instance.getParameterValue("Volume", out vol, out vol);
        return vol;
    }
}
