using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class Arrow : MonoBehaviour
{
    private int damage;
    ProjectileAudioController audio_controller;
    public void SetDamage(int dmg)
    {
        audio_controller = GameObject.Find("AudioController").GetComponent<ProjectileAudioController>();
        audio_controller.SetupSound(gameObject, PROJECTILE.arrow);
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "FlyingEnemy")
        {
            audio_controller.PlaySound(gameObject, SOUND.hit);
            Health health = other.gameObject.GetComponent<Health>();
            health.DealDamage(damage);
            audio_controller.RemoveSound(gameObject, 1.0f);
            Destroy(this.gameObject);
        }
        //if (other.gameObject.tag == "Wall")
        //{
        //    audio_controller.PlaySound(gameObject, SOUND.hit);
        //    audio_controller.RemoveSound(gameObject, 1.0f);
        //    Destroy(this.gameObject);
        //}
    }
}
