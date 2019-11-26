using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EnemyAudioController : MonoBehaviour
{  
    [Header("Spider Sounds")]
    [FMODUnity.EventRef] public string SpiderAttack;
    [FMODUnity.EventRef] public string SpiderChase;
    [FMODUnity.EventRef] public string SpiderDie;
    [Header("Bat Sounds")]
    [FMODUnity.EventRef] public string BatAttack;
    [FMODUnity.EventRef] public string BatChase;
    [FMODUnity.EventRef] public string BatDie;
    [Header("Pyromancer Sounds")]
    [FMODUnity.EventRef] public string PyroAttack;
    private float volume = 100;
    List<GameObject> enemies = new List<GameObject>();
    List<FMOD.Studio.EventInstance> enemies_events = new List<FMOD.Studio.EventInstance>();
    public void SetVolume(float vol)
    {
        volume = vol;
    }

    public void PlaySound(ENEMYTYPE enemy_type, SOUND sound_type, GameObject position)
    {      
        FMOD.Studio.EventInstance sound_event = FMODUnity.RuntimeManager.CreateInstance(SelectAudio(enemy_type, sound_type));
        sound_event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        sound_event.start();
    }

    public string SelectAudio(ENEMYTYPE enemy_type, SOUND sound_type)
    {
        if (enemy_type == ENEMYTYPE.ground)
        {
            if (sound_type == SOUND.attack)
            {
                return SpiderAttack;
            }
            else if (sound_type == SOUND.chase)
            {
                return SpiderChase;
            }
            else if (sound_type == SOUND.die)
            {
                return SpiderDie;
            }
        }
        else if (enemy_type == ENEMYTYPE.flying)
        {
            if (sound_type == SOUND.attack)
            {
                return BatAttack;
            }
            else if (sound_type == SOUND.chase)
            {
                return BatChase;
            }
            else if (sound_type == SOUND.die)
            {
                return BatDie;
            }
        }
        else if (enemy_type == ENEMYTYPE.ranged)
        {
            if (sound_type == SOUND.attack)
            {
                return PyroAttack;
            }
        }

        return "";
    }
}
