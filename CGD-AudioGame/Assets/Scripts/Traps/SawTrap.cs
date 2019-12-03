using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class SawTrap : MonoBehaviour
{
    GameObject player;
    public Transform start_point;
    public Transform end_point;
    public float move_speed = 2.5f;
    public float spin_speed = 250;
    public float raise_speed = 3.0f;
    public float raise_range = 10.0f;
    public int damage = 10;
    private bool forward = true;
    TrapAudioController audio_controller;
    bool raised = true;
    float lowered_height;
    float raised_height;
    float player_distance;
    public float volume = 100;
    bool spinning_up = false;
    bool spinning_down = false;
    // Start is called before the first frame update
    void Start()
    {    
        transform.position = start_point.position;
        lowered_height = transform.position.y - 2;
        raised_height = transform.position.y;
        player = GameObject.FindWithTag("Player");

        audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        if (audio_controller != null)
        {
            audio_controller.SetupSound(gameObject, TRAP.saw);
            audio_controller.PlaySound(TRAP.saw, gameObject);
        }
        else
        {
            Debug.Log("Audio controller not setup");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            player_distance = Vector3.Distance(transform.parent.position, player.transform.position);
        }
        else
        {
            player_distance = 0;
        }

        if (player_distance <= raise_range && !raised && !spinning_up && !spinning_down)
        {
            StartCoroutine(Raise());
        }
        else if (player_distance > raise_range && raised && !spinning_up && !spinning_down)
        {
            StartCoroutine(Lower());
        }

        transform.Rotate(spin_speed * Time.deltaTime, 0, 0);
        if (raised)
        {
            Move();
        }
    }

    IEnumerator Raise()
    {
        StartCoroutine(SpinUp());
        while (transform.position.y < raised_height)
        {
            Vector3 pos = transform.position;
            pos.y += raise_speed * Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        raised = true;
    }

    IEnumerator Lower()
    {
        StartCoroutine(SpinDown());
        while (transform.position.y > lowered_height)
        {
            Vector3 pos = transform.position;
            pos.y -= (raise_speed / 2) * Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        raised = false;
    }

    IEnumerator SpinUp()
    {
        spinning_up = true;
        while (spin_speed < 350)
        {
            if (volume < 100)
            {
                volume += 250 * Time.deltaTime;
            }
            audio_controller.SetParameter(gameObject, "Volume", volume);
            audio_controller.SetParameter(gameObject, "Pitch", volume);
            spin_speed += 250 * Time.deltaTime;
            yield return null;
        }
        audio_controller.SetParameter(gameObject, "Volume", 100);
        audio_controller.SetParameter(gameObject, "Pitch", 100);
        spin_speed = 350;
        spinning_up = false;
    }
    IEnumerator SpinDown()
    {
        spinning_down = true;
        while (spin_speed > 0)
        {
            if (volume > 0)
            {
                volume -= 250 * Time.deltaTime;
            }
            audio_controller.SetParameter(gameObject, "Volume", volume);
            audio_controller.SetParameter(gameObject, "Pitch", volume);
            spin_speed -= 250 * Time.deltaTime;
            yield return null;
        }
        audio_controller.SetParameter(gameObject, "Volume", 0);
        audio_controller.SetParameter(gameObject, "Pitch", 0);
        spin_speed = 0;
        spinning_down = false;
    }

    void Move()
    {
        float step = move_speed * Time.deltaTime;
        Transform target = null;
        if (forward)
        {
            target = end_point;
        }
        else
        {
            target = start_point;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        
        if (transform.position == target.position)
        {
            forward ^= true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "FlyingEnemy")
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.DealDamage(damage);
        }
    }
}
