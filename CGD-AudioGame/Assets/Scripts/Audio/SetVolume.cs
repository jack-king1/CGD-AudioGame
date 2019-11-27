using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public float Master = 1.0f;
    public float Music = 100.0f;
    public float Asmospheric = 100.0f;
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
        masterBus.setVolume(100 * Master);
        EnemyAudioController enemy_audio = GetComponent<EnemyAudioController>();
        enemy_audio.SetVolume(Gameplay);
    }
}
