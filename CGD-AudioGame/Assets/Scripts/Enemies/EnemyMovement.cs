using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using enums;
public class EnemyMovement : MonoBehaviour
{
    public float chase_speed = 7;
    public float patrol_speed = 3.5f;
    public float search_speed = 2.5f;
    public float hit_range = 1;
    public float detect_volume = 5;
    public float detect_range = 2;
    public float turn_speed = 5;
    public int damage = 10;
    public STATE current_state = STATE.patrol;
    private GameObject player;
    private float distance;
    public int path_index;
    private List<Transform> path_points = new List<Transform>();
    private Vector3 random_pos;
    bool searching = false;
    public float hear_volume = 0.0f;
    Movement pl_movement;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (player)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }
        Movement();      
    }

    void Movement()
    {
        hear_volume = pl_movement.FootStepVolume() - distance;
        // If player is in range, start chasing
        if ((hear_volume >= detect_volume || distance <= detect_range) && player)
        {
            current_state = STATE.chase;
        }

        // If chasing player and goes out of range, start searching
        if (current_state == STATE.chase)
        {
            searching = false;
            if (player != null)
            {
                ChasePlayer();
            }
            else
            {
                current_state = STATE.patrol;
            }

            if (hear_volume < detect_volume || distance > detect_range)
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

    void ChasePlayer()
    {
        agent.speed = chase_speed;
        TurnSmoothly(player.transform.position);
        if (distance > hit_range)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            Health pl_health = player.GetComponent<Health>();
            pl_health.DealDamage(damage);
        }
    }

    void FollowPath()
    {
        agent.speed = patrol_speed;
        if (Vector3.Distance(transform.position, path_points[path_index].position) < 1)
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
            TurnSmoothly(path_points[path_index].position);        
            agent.SetDestination(path_points[path_index].position);
        }
    }

    void RandomMovement()
    {
        agent.speed = search_speed;
        if (!searching)
        {
            StartCoroutine(SearchTimer());
        }

        if (transform.position == random_pos)
        {
            random_pos = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 0, Random.Range(transform.position.z - 5, transform.position.z + 5));
        }
        TurnSmoothly(random_pos);
        agent.SetDestination(random_pos);
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

    void TurnSmoothly(Vector3 target)
    {
        var target_rotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, turn_speed * Time.deltaTime);
    }
}