using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private GameObject owner;
    private FMOD.Studio.EventInstance attack_event;
    private FMOD.Studio.EventInstance chase_event;
    private FMOD.Studio.EventInstance die_event;
    private float vol_multi = 1;

    public EnemySounds(GameObject obj, string attack, string chase, string die)
    {
        owner = obj;
        attack_event = FMODUnity.RuntimeManager.CreateInstance(attack);
        chase_event = FMODUnity.RuntimeManager.CreateInstance(chase);
        die_event = FMODUnity.RuntimeManager.CreateInstance(die);
    }

    public GameObject Owner() => owner;



    public void SetVolume(float volume)
    {
        attack_event.setParameterValue("Volume", volume * vol_multi);
        chase_event.setParameterValue("Volume", volume * vol_multi);
        die_event.setParameterValue("Volume", volume * vol_multi);
    }

    public float GetVolume()
    {
        float vol;
        chase_event.getParameterValue("Volume", out vol, out vol);
        return vol;
    }

    public FMOD.Studio.EventInstance GetAttack() => attack_event;
    public FMOD.Studio.EventInstance GetChase() => chase_event;
    public FMOD.Studio.EventInstance GetDeath() => die_event;

    public float GetVolMultiplier() => vol_multi;
    public void SetVolMultiplier(float multi) => vol_multi = multi;   
}
