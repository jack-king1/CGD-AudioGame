using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class EnemyMovement : MonoBehaviour
{
    public float chase_speed = 10;
    public float patrol_speed = 5;
    public float hit_range = 1;
    public float detect_range = 10;
    public int damage = 10;
    public STATE current_state = STATE.patrol;
    private GameObject player;
    private float distance;
    private int path_index;
    private List<Transform> path_points = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.tag == "Path")
            {
                path_points.Add(transform.parent.GetChild(i).gameObject.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < detect_range)
        {
            current_state = STATE.chase;
        }
        else
        {
            current_state = STATE.patrol;
        }

        if (current_state == STATE.chase)
        {
            MoveToPlayer();
        }
        else if (current_state == STATE.patrol)
        {
            FollowPath();
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
}
