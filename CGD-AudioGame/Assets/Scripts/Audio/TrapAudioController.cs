using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class TrapAudioController : MonoBehaviour
{
    List<TrapSounds> sounds = new List<TrapSounds>();
    [FMODUnity.EventRef] public string SpikeTrap;
    [FMODUnity.EventRef] public string SawTrap;
    [FMODUnity.EventRef] public string ArrowTrap;
    private float last_vol;
    private float volume;

    private void Start() => last_vol = volume;

    public void SetVolume(float vol) => volume = vol;

    private void Update()
    {
        last_vol = volume;
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].SetVolume(volume);
            Debug.Log(sounds[i].GetVolume() + " TRAP VOL");
        }
    }

    public void SetupSound(GameObject owner, TRAP trap_type)
    {
        if (trap_type == TRAP.spike)
        {
            sounds.Add(new TrapSounds(owner, SpikeTrap));
        }
        else if (trap_type == TRAP.saw)
        {
            sounds.Add(new TrapSounds(owner, SawTrap));
        }
        else if (trap_type == TRAP.arrow)
        {
            sounds.Add(new TrapSounds(owner, ArrowTrap));
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


    IEnumerator SmoothMultiChange(TrapSounds sound, float level)
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
}
