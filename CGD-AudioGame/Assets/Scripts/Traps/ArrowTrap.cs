using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class ArrowTrap : MonoBehaviour
{
    public GameObject arrow_prefab;
    public float arrow_speed = 5;
    public float cooldown = 3;
    public int damage = 100;
    private GameObject button;
    private Transform target;
    bool can_fire = true;
    TrapAudioController audio_controller;
    // Start is called before the first frame update
    void Start()
    {
        
        button = transform.GetChild(0).gameObject;
        target = transform.GetChild(1);

        audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        if (audio_controller != null)
        {
            audio_controller.SetupSound(gameObject, enums.TRAP.arrow);
        }
        button = transform.GetChild(0).gameObject;
        target = transform.GetChild(1);
    }

    public void FireArrow()
    {
        if (can_fire)
        {
            StartCoroutine(ArrowSequence());
        }
    }

    IEnumerator ArrowSequence()
    {
        if (audio_controller != null)
        {
            audio_controller.PlaySound(TRAP.arrow, gameObject);
        }
        can_fire = false;
        GameObject arrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity);
        arrow.transform.LookAt(target.position);
        Arrow arrow_scr = arrow.GetComponent<Arrow>();
        arrow_scr.SetDamage(damage);
        while (arrow.transform.position != target.position && arrow != null)
        {
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, target.position, arrow_speed * Time.deltaTime);
            yield return null;
        }
        if (arrow != null)
        {
            Destroy(arrow.gameObject);
        }
        yield return new WaitForSeconds(cooldown);
        can_fire = true;
    }
}
