using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds
{
    GameObject owner;
    FMOD.Studio.EventInstance attack_event;
    FMOD.Studio.EventInstance chase_event;
    FMOD.Studio.EventInstance die_event;

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
        Debug.Log("YES");
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
}
