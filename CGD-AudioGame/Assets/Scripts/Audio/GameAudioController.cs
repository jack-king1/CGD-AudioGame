using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    private float music_volume;
    private float atmospheric_volume;
    [FMODUnity.EventRef] public string music;
    [FMODUnity.EventRef] public string atmospheric;
    [FMODUnity.EventRef] public string win_jingle;
    [FMODUnity.EventRef] public string lose_jingle;
    FMOD.Studio.EventInstance music_event;
    FMOD.Studio.EventInstance atmospheric_event;

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
    void Start()
    {
        music_event = FMODUnity.RuntimeManager.CreateInstance(music);
        atmospheric_event = FMODUnity.RuntimeManager.CreateInstance(atmospheric);
    }
}
