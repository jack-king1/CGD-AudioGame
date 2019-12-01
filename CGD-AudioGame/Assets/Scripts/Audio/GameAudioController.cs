using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    private float music_volume;
    private float atmospheric_volume;
    private float game_volume;
    [FMODUnity.EventRef] public string music;
    [FMODUnity.EventRef] public string atmospheric;
    [FMODUnity.EventRef] public string win_jingle;
    [FMODUnity.EventRef] public string lose_jingle;
    FMOD.Studio.EventInstance music_event;
    FMOD.Studio.EventInstance atmospheric_event;
    FMOD.Studio.EventInstance win_event;
    FMOD.Studio.EventInstance lose_event;
    public void SetMusicVolume(float vol)
    {
        music_volume = vol;
        music_event.setParameterValue("Volume", music_volume);
    }
    public void SetAtmosphericVolume(float vol)
    {
        atmospheric_volume = vol;
        atmospheric_event.setParameterValue("Volume", atmospheric_volume);
    }
    public void SetGameVolume(float vol)
    {
        game_volume = vol;
        win_event.setParameterValue("Volume", game_volume);
        lose_event.setParameterValue("Volume", game_volume);
    }
    void Start()
    {
        music_event = FMODUnity.RuntimeManager.CreateInstance(music);
        atmospheric_event = FMODUnity.RuntimeManager.CreateInstance(atmospheric);
        music_event.start();
        atmospheric_event.start();
    }

    public void PlayWinJingle()
    {
        win_event.start();
    }

    public void PlayLoseJingle()
    {
        lose_event.start();
    }
}
