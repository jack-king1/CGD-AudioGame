using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public float volume = 100.0f;
    string masterBusString = "Bus:/";
    FMOD.Studio.Bus masterBus;
    [FMODUnity.EventRef]
    public string event_path;
    FMOD.Studio.EventInstance sound_event;
    // Start is called before the first frame update
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(volume);
        sound_event = FMODUnity.RuntimeManager.CreateInstance(event_path);
        sound_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        sound_event.start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
