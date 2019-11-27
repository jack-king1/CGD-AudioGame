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
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Fire(GameObject target)
    {
        if (can_fire)
        {
            StartCoroutine(FireSequence(target));
        }
    }

    IEnumerator FireSequence(GameObject target)
    {       
        anim.SetBool("Attack", true);
        can_fire = false;
        yield return new WaitForSeconds(0.5f);
        Vector3 move_dir = (target.transform.position - transform.position).normalized;
        Vector3 staff_pos = transform.Find("Hips/Staff/StaffTarget").position;
        GameObject fireball = Instantiate(fireball_prefab, staff_pos, Quaternion.identity);
        Fireball fireball_scr = fireball.GetComponent<Fireball>();
        fireball_scr.Fire(damage, shot_speed, move_dir);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(cooldown);
        can_fire = true;
    }
}
