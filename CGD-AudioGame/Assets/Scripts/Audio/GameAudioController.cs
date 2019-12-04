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
    [FMODUnity.EventRef] public string heartbeat;
    [FMODUnity.EventRef] public string coins;
    [FMODUnity.EventRef] public string chest;
    [FMODUnity.EventRef] public string note;
    FMOD.Studio.EventInstance music_event;
    FMOD.Studio.EventInstance atmospheric_event;
    FMOD.Studio.EventInstance win_event;
    FMOD.Studio.EventInstance lose_event;
    FMOD.Studio.EventInstance heartbeat_event;
    FMOD.Studio.EventInstance coins_event;
    FMOD.Studio.EventInstance chest_event;
    FMOD.Studio.EventInstance note_event;
    GameObject camera;
    public List<GameObject> enemies = new List<GameObject>();
    GameObject player;
    float lowest_distance;

    public void SetMusicVolume(float vol) => music_volume = vol;
    public void SetAtmosphericVolume(float vol) => atmospheric_volume = vol;
    
    public void SetGameVolume(float vol)
    {
        game_volume = vol;
        win_event.setParameterValue("Volume", game_volume);
        lose_event.setParameterValue("Volume", game_volume);
    }
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        music_event = FMODUnity.RuntimeManager.CreateInstance(music);
        atmospheric_event = FMODUnity.RuntimeManager.CreateInstance(atmospheric);
        heartbeat_event = FMODUnity.RuntimeManager.CreateInstance(heartbeat);
        coins_event = FMODUnity.RuntimeManager.CreateInstance(coins);
        chest_event = FMODUnity.RuntimeManager.CreateInstance(chest);
        note_event = FMODUnity.RuntimeManager.CreateInstance(note);
        heartbeat_event.start();
        music_event.start();
        atmospheric_event.start();
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
    }

    private void Update()
    {
        music_event.setParameterValue("Volume", music_volume);
        atmospheric_event.setParameterValue("Volume", atmospheric_volume);
        atmospheric_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(camera));
        music_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(camera));
        heartbeat_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(camera));
        lowest_distance = HeartbeatDetect();
        if (enemies.Count > 0)
        {
            heartbeat_event.setParameterValue("Volume", (5 / lowest_distance) * game_volume);
        }
        else
        {
            heartbeat_event.setParameterValue("Volume", 0);
        }
    }

    public void PlayWinJingle() => win_event.start();
    public void PlayLoseJingle() => lose_event.start();

    public void PlayCoinSound(GameObject chest)
    {
        coins_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(chest));
        chest_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(chest));
        StartCoroutine(ChestSequence());
    }

    public void PlayNoteSound(GameObject location, float pitch)
    {
        note_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(location));
        note_event.setParameterValue("Pitch", pitch);
        note_event.start();
    }

    public void PlayNoteSequence(GameObject location, float[] pitches)
    {
        note_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(location));
        StartCoroutine(NoteSequence(pitches));
    }

    IEnumerator NoteSequence(float[] pitches)
    {
        for (int i = 0; i < 4; i++)
        {
            note_event.setParameterValue("Pitch", pitches[i]);
            note_event.start();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator ChestSequence()
    {
        Debug.Log("CHEST SEQUENCE");
        chest_event.start();
        yield return new WaitForSeconds(1.5f);
        chest_event.start();
        yield return new WaitForSeconds(0.6f);
        int num_loops = 3;
        for (int i = 0; i < num_loops; i++)
        {
            coins_event.start();
            yield return new WaitForSeconds(0.3f);
        }
    }


    float HeartbeatDetect()
    {
        List<float> distances = new List<float>();
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
            distances.Add(dist);    
        }
        distances.Sort();
        if (distances.Count > 0)
        {
            return distances[0];
        }
        return 0;
    }
}
