using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject fireball_prefab;
    public float shot_speed = 5;
    public float cooldown = 4;
    public int damage;
    bool can_fire = true;

    public void Fire(Vector3 target)
    {
        if (can_fire)
        {
            StartCoroutine(FireSequence(target));
        }
    }

    IEnumerator FireSequence(Vector3 target)
    {
        can_fire = false;
        Vector3 move_dir = (target - transform.position).normalized;
        GameObject fireball = Instantiate(fireball_prefab, transform.position, Quaternion.identity);
        Fireball fireball_scr = fireball.GetComponent<Fireball>();
        fireball_scr.Fire(damage, shot_speed, move_dir);
        yield return new WaitForSeconds(cooldown);
        can_fire = true;
    }
}
