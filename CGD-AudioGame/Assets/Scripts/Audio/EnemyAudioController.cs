using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EnemyAudioController : MonoBehaviour
{
    [FMODUnity.EventRef] public string SpiderAttack;
    [FMODUnity.EventRef] public string SpiderChase;
    [FMODUnity.EventRef] public string SpiderDie;
    [FMODUnity.EventRef] public string BatAttack;
    [FMODUnity.EventRef] public string BatChase;
    [FMODUnity.EventRef] public string BatDie;

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

        return "";
    }
}
