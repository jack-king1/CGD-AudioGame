using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EnemyAudioController : MonoBehaviour
{
    public List<EnemySounds> sounds = new List<EnemySounds>();
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


    public void SetVolume(float vol)
    {
        volume = vol;
    }

    public void PlaySound(GameObject owner, SOUND sound_type)
    {
        sounds[0].GetAttack().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[0].Owner()));
        sounds[0].GetAttack().start();
        for (int i = 0; i < sounds.Count; i++)
        {
            if (owner == sounds[i].Owner())
            {
                if (sound_type == SOUND.attack)
                {
                    sounds[i].GetAttack().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
                    sounds[i].GetAttack().start();
                }
                else if (sound_type == SOUND.chase)
                {
                    sounds[i].GetChase().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[i].Owner()));
                    sounds[i].GetChase().start();
                }
                else if (sound_type == SOUND.die)
                {
                    sounds[i].GetDeath().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sounds[0].Owner()));
                    sounds[i].GetDeath().start();
                }
            }
        }
    }

    public void SetupSound(GameObject owner, ENEMYTYPE enemy_type)
    {
        sounds.Add(new EnemySounds(owner, SelectAudio(enemy_type, SOUND.attack), SelectAudio(enemy_type, SOUND.chase), SelectAudio(enemy_type, SOUND.die)));
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

        return "No Sound";
    }
}
