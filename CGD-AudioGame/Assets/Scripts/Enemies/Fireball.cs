using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private int damage;
    public float kill_timer = 5;
    bool moving = true;
    public void Fire(int dmg, float shot_speed, Vector3 target)
    {
        damage = dmg;
        StartCoroutine(Move(target, shot_speed));
    }

    IEnumerator Move(Vector3 target, float shot_speed)
    {
        float timer = 5;
        while (moving)
        {
            transform.position += target * shot_speed * Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            StartCoroutine(Explode());
        }

        if (other.gameObject.tag == "Player")
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.DealDamage(damage);
            StartCoroutine(Explode());
        }       
    }

    IEnumerator Explode()
    {
        moving = false;
        // !!RUN EXPLODE PARTICLE HERE!!
        yield return new WaitForSeconds(0.5F);
        Destroy(this.gameObject);
    }
}
