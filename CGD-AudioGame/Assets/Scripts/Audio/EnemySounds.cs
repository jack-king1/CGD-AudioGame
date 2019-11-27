using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds
{
    GameObject owner;
    FMOD.Studio.EventInstance attack_event;
    FMOD.Studio.EventInstance chase_event;
    FMOD.Studio.EventInstance die_event;
    FMOD.Studio.ParameterInstance[] vol_instance = new FMOD.Studio.ParameterInstance[3];
    private void Update()
    {
        attack_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(owner));
        chase_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(owner));
        die_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(owner));
    }

    public EnemySounds(GameObject obj, string attack, string chase, string die)
    {
        owner = obj;
        attack_event = FMODUnity.RuntimeManager.CreateInstance(attack);
        chase_event = FMODUnity.RuntimeManager.CreateInstance(chase);
        die_event = FMODUnity.RuntimeManager.CreateInstance(die);
        attack_event.getParameter("Volume", out vol_instance[0]);
        chase_event.getParameter("Volume", out vol_instance[1]);
        die_event.getParameter("Volume", out vol_instance[2]);
    }

    public GameObject Owner()
    {
        return owner;
    }
    public FMOD.Studio.EventInstance GetAttack()
    {
        return attack_event;
    }
    public FMOD.Studio.EventInstance GetChase()
    {
        return chase_event;
    }
    public FMOD.Studio.EventInstance GetDeath()
    {
        return die_event;
    }

    public void SetVolume(float volume)
    {
        attack_event.setParameterValue("Volume", volume);
        chase_event.setParameterValue("Volume", volume);
        die_event.setParameterValue("Volume", volume);
    }

    public float GetVolume()
    {
        float vol;
        chase_event.getParameterValue("Volume", out vol, out vol);
        return vol;
    }
}
