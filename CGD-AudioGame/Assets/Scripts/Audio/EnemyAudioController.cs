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
    private float last_vol;
    public float volume = 1;

    private void Start()
    {
        last_vol = volume;
    }

    private void Update()
    {
        if (volume != last_vol)
        {
            last_vol = volume;
            for (int i = 0; i < sounds.Count; i++)
            {
                sounds[i].SetVolume(volume);
                Debug.Log(sounds[i].GetVolume());
            }
        }
    }

    public void SetVolume(float vol)
    {
        volume = vol;
    }

    public void PlaySound(GameObject owner, SOUND sound_type)
    {
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
        Debug.Log(sounds[0].GetVolume());
    }

    public void SetParameter(GameObject owner, SOUND sound_type, string param, float val)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if  (sounds[i].Owner() == owner)
            {
                if (sound_type == SOUND.attack)
                {
                    sounds[i].GetAttack().setParameterValue(param, val);
                }
                else if (sound_type == SOUND.chase)
                {
                    sounds[i].GetChase().setParameterValue(param, val);
                }
                else if (sound_type == SOUND.die)
                {
                    sounds[i].GetDeath().setParameterValue(param, val);
                }             
            }
        }      
    }

    public float GetParameter(GameObject owner, SOUND sound_type, string param)
    {
        float val = 0;
        float val2 = 0;
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].Owner() == owner)
            {
                if (sound_type == SOUND.attack)
                {
                    sounds[i].GetAttack().getParameterValue(param, out val, out val2);
                }
                else if (sound_type == SOUND.chase)
                {
                    sounds[i].GetChase().getParameterValue(param, out val, out val2);
                }
                else if (sound_type == SOUND.die)
                {
                    sounds[i].GetDeath().getParameterValue(param, out val, out val2);
                }
            }
        }
        return val;
    }

    public void SetupSound(GameObject owner, ENEMYTYPE enemy_type)
    {
        sounds.Add(new EnemySounds(owner, SelectAudio(enemy_type, SOUND.attack), SelectAudio(enemy_type, SOUND.chase), SelectAudio(enemy_type, SOUND.die)));
    }

    public void RemoveSound(GameObject owner)
    {
        for (int i = 0; i < sounds.Count; i++)
        {   
            if (sounds[i].Owner() == owner)
            {
                sounds.Remove(sounds[i]);
            }
        }
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
