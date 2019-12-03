using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class ProjectileAudioController : MonoBehaviour
{
    List<ProjectileSounds> sounds = new List<ProjectileSounds>();
    [FMODUnity.EventRef] public string fireball_loop;
    [FMODUnity.EventRef] public string fireball_hit;
    [FMODUnity.EventRef] public string arrow_hit;
    float game_volume;
    private void Update()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != null)
            {               
                sounds[i].GetLoop().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
            }
        }
    }

    public void SetGameVolume(float vol)
    {
        game_volume = vol;
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != null)
            {
                sounds[i].GetLoop().setParameterValue("Volume", game_volume);
                sounds[i].GetHit().setParameterValue("Volume", game_volume);
            }
        }
    }

    public void SetupSound(GameObject owner, PROJECTILE projectile)
    {
        if (projectile == PROJECTILE.arrow)
        {
            sounds.Add(new ProjectileSounds(owner, arrow_hit, ""));
        }
        else if (projectile == PROJECTILE.fireball)
        {
            sounds.Add(new ProjectileSounds(owner, fireball_hit, fireball_loop));
        }
        sounds[sounds.Count - 1].GetLoop().setParameterValue("Volume", game_volume);
        sounds[sounds.Count - 1].GetHit().setParameterValue("Volume", game_volume);
    }

    public void RemoveSound(GameObject owner, float delay)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                StartCoroutine(RemoveRoutine(delay, sounds[i]));
            }
        }
    }

    IEnumerator RemoveRoutine(float delay, ProjectileSounds sound)
    {
        yield return new WaitForSeconds(delay);
        sounds.Remove(sound);
    }

    public void PlaySound(GameObject owner, SOUND sound)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                if (sound == SOUND.hit)
                {
                    sounds[i].GetHit().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
                    sounds[i].GetHit().start();
                }
                if (sound == SOUND.loop)
                {
                    sounds[i].GetLoop().start();
                }
            }
        }
    }
}
