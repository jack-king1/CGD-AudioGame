using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class FootstepAudioController : MonoBehaviour
{
    List<FootstepSounds> sounds = new List<FootstepSounds>();
    [FMODUnity.EventRef] public string player_footstep;
    [FMODUnity.EventRef] public string spider_footstep;
    [FMODUnity.EventRef] public string bat_flap;

    private void Update()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i] != null)
            {
                sounds[i].GetEvent().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
            }
        }
    }

    public void PlaySound(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                sounds[i].GetEvent().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
                sounds[i].GetEvent().start();
            }
        }
    }

    public void StopSound(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                sounds[i].GetEvent().stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }

    public bool IsPlaying(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                FMOD.Studio.PLAYBACK_STATE playbackState;
                sounds[i].GetEvent().getPlaybackState(out playbackState);
                return playbackState != FMOD.Studio.PLAYBACK_STATE.STOPPED;
            }
        }
        return false;
    }

    public void SetupSound(GameObject owner, FOOTSTEP type)
    {
        if (type == FOOTSTEP.player)
        {
            sounds.Add(new FootstepSounds(owner, player_footstep));
        }
        else if (type == FOOTSTEP.spider)
        {
            sounds.Add(new FootstepSounds(owner, spider_footstep));
        }
        else if (type == FOOTSTEP.pyro)
        {
            sounds.Add(new FootstepSounds(owner, player_footstep));
        }
        else if (type == FOOTSTEP.bat)
        {
            sounds.Add(new FootstepSounds(owner, bat_flap));
        }
    }

    public void RemoveSound(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                sounds.Remove(sounds[i]);
            }
        }
    }
}
