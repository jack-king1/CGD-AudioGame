using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public Transform start_point;
    public Transform end_point;
    public float move_speed = 2.5f;
    public float spin_speed = 250;
    public int damage = 10;
    private bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start_point.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spin_speed * Time.deltaTime);
        Move();
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
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.DealDamage(damage);
        }
    }
}
