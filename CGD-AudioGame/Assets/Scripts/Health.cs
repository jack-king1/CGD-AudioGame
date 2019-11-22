using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int start_health = 100;
    public int health;
    public float invulnerable_time = 0.5f;
    bool can_damage = true;
    // Start is called before the first frame update
    void Start()
    {
        health = start_health;
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " died");
        }
    }

    public void DealDamage(int damage)
    {
        if (can_damage)
        {
            StartCoroutine(DamageRoutine(damage));
        }
    }

    IEnumerator DamageRoutine(int damage)
    {
        can_damage = false;
        health -= damage;
        yield return new WaitForSeconds(invulnerable_time);
        can_damage = true;
    }
}
