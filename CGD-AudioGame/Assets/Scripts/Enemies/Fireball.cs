using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class Fireball : MonoBehaviour
{
    public GameObject explosion_prefab;
    private int damage;
    public float kill_timer = 5;
    bool moving = true;
    ParticleSystem fireball_p;
    ProjectileAudioController audio_controller;
    private void Start()
    {
        audio_controller = GameObject.Find("AudioController").GetComponent<ProjectileAudioController>();
        audio_controller.SetupSound(gameObject, PROJECTILE.fireball);
        audio_controller.PlaySound(gameObject, SOUND.loop);
    }

    public void Fire(int dmg, float shot_speed, Vector3 target)
    {
        fireball_p = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();        
        damage = dmg;

        StartCoroutine(Move(target, shot_speed));
    }

    IEnumerator Move(Vector3 target, float shot_speed)
    {
        var em = fireball_p.emission;
        fireball_p.Play();
        em.enabled = true;
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
        audio_controller.PlaySound(gameObject, SOUND.hit);
        yield return new WaitForSeconds(0.1f);
        var em = fireball_p.emission;
        em.enabled = false;
        GameObject explosion = Instantiate(explosion_prefab, transform.position, Quaternion.identity);
        ParticleSystem explosion_p = explosion.GetComponent<ParticleSystem>();
        DeleteAfterDelay delete = explosion.GetComponent<DeleteAfterDelay>();
        em = explosion_p.emission;
        em.enabled = true;
        explosion_p.Play();
        delete.StartDelete(0.5f);
        yield return new WaitForSeconds(0.4f);
        audio_controller.RemoveSound(gameObject, 0);
        Destroy(this.gameObject);     
    }
}
