using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        audio_controller.SetupSound(gameObject, enums.TRAP.saw);
        transform.position = start_point.position;
        lowered_height = transform.position.y - 2;
        raised_height = transform.position.y;
        player = GameObject.FindWithTag("Player");
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

        if (player_distance <= raise_range && !raised)
        {
            StartCoroutine(Raise());
        }
        else if (player_distance > raise_range && raised)
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
        while (spin_speed < 350)
        {
            spin_speed += 50 * Time.deltaTime;
            yield return null;
        }
        spin_speed = 350;
    }
    IEnumerator SpinDown()
    {
        while (spin_speed > 0)
        {
            spin_speed -= 10 * Time.deltaTime;
            yield return null;
        }
        spin_speed = 0;
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
