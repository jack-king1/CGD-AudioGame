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
    EnemyAudioController enemy_audio;
    TrapAudioController trap_audio;
    GameAudioController game_audio;
    ProjectileAudioController projectile_audio;
    PickupAudioController pickup_audio;
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(100 * Master);
        trap_audio = GetComponent<TrapAudioController>();
        projectile_audio = GetComponent<ProjectileAudioController>();
        pickup_audio = GetComponent<PickupAudioController>();
        enemy_audio = GetComponent<EnemyAudioController>();
        game_audio = GetComponent<GameAudioController>();
    }

    void Update()
    {
        masterBus.setVolume(Master);
        enemy_audio.SetVolume(100 * Gameplay);
        trap_audio.SetVolume(100 * Gameplay);
        game_audio.SetGameVolume(100 * Gameplay);
        game_audio.SetAtmosphericVolume(100 * Asmospheric);
        game_audio.SetMusicVolume(100 * Music);
        projectile_audio.SetGameVolume(100 * Gameplay);
        pickup_audio.SetVolume(100 * Gameplay);
    }
}
