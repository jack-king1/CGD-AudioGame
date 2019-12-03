using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject blood_prefab;
    public int start_health = 100;
    public int health;
    public float invulnerable_time = 0.5f;
    bool can_damage = true;
    bool is_dead = false;
    // Start is called before the first frame update
    void Start()
    {
        health = start_health;
    }

    void Update()
    {
        if (health <= 0 && !is_dead)
        {
            StartCoroutine(DeathRoutine());
        }
    }

    public bool DealDamage(int damage)
    {
        if (can_damage)
        {
            StartCoroutine(DamageRoutine(damage));
            return true;
        }
        return false;
    }

    IEnumerator DamageRoutine(int damage)
    {
        can_damage = false;
        health -= damage;
        yield return new WaitForSeconds(invulnerable_time);
        can_damage = true;
    }

    IEnumerator DeathRoutine()
    {
        is_dead = true;
        GameObject blood = Instantiate(blood_prefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        ParticleSystem blood_p = blood.GetComponent<ParticleSystem>();
        DeleteAfterDelay delete = blood.GetComponent<DeleteAfterDelay>();
        var em = blood_p.emission;
        em.enabled = true;
        blood_p.Play();
        delete.StartDelete(0.5f);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        yield return null;
    }

    private void OnDestroy()
    {
        if (gameObject.tag == "Enemy")
        {
            if(GameObject.Find("AudioController").GetComponent<EnemyAudioController>())
            {
                EnemyAudioController audio_controller = GameObject.Find("AudioController").GetComponent<EnemyAudioController>();
                audio_controller.RemoveSound(gameObject);
            }         
        }
    }
}
