using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class TrapAudioController : MonoBehaviour
{
    List<SingleSound> sounds = new List<SingleSound>();
    [FMODUnity.EventRef] public string SpikeTrap;
    [FMODUnity.EventRef] public string SawTrap;
    [FMODUnity.EventRef] public string ArrowTrap;
    [FMODUnity.EventRef] public string ArrowButton;
    private float last_vol;
    private float volume;

    private void Start() => last_vol = volume;

    public void SetVolume(float vol) => volume = vol;

    private void Update()
    {
        last_vol = volume;
        for (int i = 0; i < sounds.Count; i++)
        {
            //sounds[i].SetVolume(volume);
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

    public void PlaySound(TRAP type, GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].GetOwner() == owner)
            {
                sounds[i].GetEvent().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
                sounds[i].GetEvent().start();
            }
        }
    }

    public void SetupSound(GameObject owner, TRAP trap_type)
    {
        if (trap_type == TRAP.spike)
        {
            sounds.Add(new SingleSound(owner, SpikeTrap));
        }
        else if (trap_type == TRAP.saw)
        {
            sounds.Add(new SingleSound(owner, SawTrap));
        }
        else if (trap_type == TRAP.arrow)
        {
            sounds.Add(new SingleSound(owner, ArrowTrap));
        }
        else if (trap_type == TRAP.arrow_btn)
        {
            sounds.Add(new SingleSound(owner, ArrowButton));
        }
    }

    public void SetVolMultiplier(GameObject owner, bool reduced)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                if (reduced)
                {
                    StartCoroutine(SmoothMultiChange(sounds[i], 0.6f));
                }
                else
                {
                    StartCoroutine(SmoothMultiChange(sounds[i], 1.0f));
                }
            }
        }
    }



    IEnumerator SmoothMultiChange(SingleSound sound, float level)
    {
        float multi = sound.GetVolMultiplier();

        if (level > multi)
        {
            while (multi < level)
            {
                if (multi < 1)
                {
                    multi += 0.5f * Time.deltaTime;
                    sound.SetVolMultiplier(multi);
                }
                yield return null;
            }
        }
        else if (level < multi)
        {
            while (multi > level)
            {
                if (multi > 0.6)
                {
                    multi -= 0.5f * Time.deltaTime;
                    sound.SetVolMultiplier(multi);
                }
                yield return null;
            }
        }
        sound.SetVolMultiplier(level);
    }

    public float GetParameter(GameObject owner, string param)
    {
        float val = 0;
        float val2 = 0;
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                sounds[i].GetEvent().getParameterValue(param, out val, out val2);              
            }
        }
        return val;
    }
}
