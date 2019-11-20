using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    bool following = true;
    float distance;
    public float chase_speed = 10;
    public float patrol_speed = 5;
    public float hit_range = 1;
    public int damage = 10;
    int path_index;
    public List<Transform> path_points = new List<Transform>();
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
        if (following)
        {
            MoveToPlayer();
        }
        else
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
            transform.position = Vector3.MoveTowards(transform.position, path_points[path_index].position, step);
        }
    }
}
