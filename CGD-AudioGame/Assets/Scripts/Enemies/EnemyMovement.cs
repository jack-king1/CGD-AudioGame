using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EnemyMovement : MonoBehaviour
{
    public float chase_speed = 10;
    public float patrol_speed = 5;
    public float hit_range = 1;
    public float detect_range = 5;
    public int damage = 10;
    public STATE current_state = STATE.patrol;
    private GameObject player;
    private float distance;
    private int path_index;
    private List<Transform> path_points = new List<Transform>();
    private Vector3 random_pos;
    bool searching = false;
    public float hear_volume = 0.0f;
    Movement pl_movement;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pl_movement = player.GetComponent<Movement>();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.tag == "Path")
            {
                path_points.Add(transform.parent.GetChild(i).gameObject.transform);
            }
        }
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        Movement();      
    }

    void Movement()
    {
        hear_volume = pl_movement.FootStepVolume() - distance;
        // If player is in range, start chasing
        if (hear_volume > detect_range)
        {
            current_state = STATE.chase;
        }

        // If chasing player and goes out of range, start searching
        if (current_state == STATE.chase)
        {
            searching = false;
            MoveToPlayer();

            if (hear_volume < detect_range)
            {
                current_state = STATE.search;
            }
        }
        else if (current_state == STATE.patrol)
        {
            searching = false;
            FollowPath();
        }
        else if (current_state == STATE.search)
        {
            RandomMovement();
        }
    }

    void MoveToPlayer()
    {
        float step = chase_speed * Time.deltaTime;
        transform.LookAt(player.transform);
        if (distance > hit_range)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
        else
        {
            Health pl_health = player.GetComponent<Health>();
            pl_health.DealDamage(damage);
        }
    }

    void FollowPath()
    {
        float step = patrol_speed * Time.deltaTime;
        if (transform.position == path_points[path_index].position)
        {
            if (path_index == path_points.Count - 1)
            {
                path_index = 0;
            }
            else
            {
                path_index++;
            }
        }
        else
        {
            transform.LookAt(path_points[path_index]);
            transform.position = Vector3.MoveTowards(transform.position, path_points[path_index].position, step);
        }
    }

    void RandomMovement()
    {
        float step = patrol_speed * Time.deltaTime;
        if (!searching)
        {
            StartCoroutine(SearchTimer());
        }

        if (transform.position == random_pos)
        {
            random_pos = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 0, Random.Range(transform.position.z - 5, transform.position.z + 5));
        }
        transform.LookAt(random_pos);
        transform.position = Vector3.MoveTowards(transform.position, random_pos, step);
    }

    IEnumerator SearchTimer()
    {
        searching = true;
        random_pos = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 0, Random.Range(transform.position.z - 5, transform.position.z + 5));
        yield return new WaitForSeconds(10);
        if (current_state == STATE.search)
        {
            current_state = STATE.patrol;
        }
    }
}
