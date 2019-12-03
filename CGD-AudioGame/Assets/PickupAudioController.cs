using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class PickupAudioController : MonoBehaviour
{
    GameObject owner;
    List<SingleSound> sounds = new List<SingleSound>();
    [FMODUnity.EventRef] public string coin;
    [FMODUnity.EventRef] public string mana;
    private float last_vol;
    private float volume;

    private void Start() => last_vol = volume;

    public void SetVolume(float vol)
    {
        volume = vol;
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != null)
            {
                sounds[i].SetVolume(volume);
            }
        }
    }

    private void Update()
    {
        last_vol = volume;
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].SetVolume(volume);
        }
    }

    public void SetParameter(GameObject owner, string param, float val)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].GetOwner() == owner)
            {
                sounds[i].GetEvent().setParameterValue(param, val);
            }
        }
    }

    public void PlaySound(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].GetOwner() == owner)
            {
                sounds[i].GetEvent().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(owner));
                sounds[i].GetEvent().start();
            }
        }
    }

    public void SetupSound(GameObject owner, PICKUP type)
    {
        Debug.Log("SETUP PICKUP");
        if (type == PICKUP.coin)
        {
            sounds.Add(new SingleSound(owner, coin));
        }
        else if (type == PICKUP.mana)
        {
            sounds.Add(new SingleSound(owner, mana));
        }
    }
}
