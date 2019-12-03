using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public float Master = 1.0f;
    public float Music = 1.0f;
    public float Asmospheric = 1.0f;
    public float Gameplay = 1.0f;
    string masterBusString = "Bus:/";
    FMOD.Studio.Bus masterBus;

    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(100 * Master);
    }

    void Update()
    {
        masterBus.setVolume(Master);
        EnemyAudioController enemy_audio = GetComponent<EnemyAudioController>();
        enemy_audio.SetVolume(100 * Gameplay * Master);
        TrapAudioController trap_audio = GetComponent<TrapAudioController>();
        trap_audio.SetVolume(100 * Gameplay * Master);
        GameAudioController game_audio = GetComponent<GameAudioController>();
        game_audio.SetGameVolume(100 * Gameplay * Master);
        game_audio.SetAtmosphericVolume(100 * Asmospheric * Master);
        game_audio.SetMusicVolume(100 * Music * Master);
    }
}
