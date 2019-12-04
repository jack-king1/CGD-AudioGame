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
    public float search_radius = 10f;
    public float hit_range = 1;
    public float detect_volume = 5;
    public float detect_range = 10;
    public float turn_speed = 5;
    public int damage = 100;
    public STATE current_state = STATE.patrol;
    public GameObject player;
    public float distance;
    public int path_index;
    private List<Transform> path_points = new List<Transform>();
    public Vector3 random_pos;
    bool searching = false;
    public float hear_volume;
    Movement pl_movement;
    NavMeshAgent agent;
    public ENEMYTYPE type;
    Animator anim;
    EnemyAudioController audio_controller;
    FootstepAudioController footstep_controller;
    public LayerMask sight_layer_mask;
    bool chase_played = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        pl_movement = player.GetComponent<Movement>();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.tag == "Path")
            {
                path_points.Add(transform.parent.GetChild(i).gameObject.transform);
            }
        }

        audio_controller = GameObject.Find("AudioController").GetComponent<EnemyAudioController>();
        audio_controller.SetupSound(gameObject, type);
        footstep_controller = GameObject.Find("AudioController").GetComponent<FootstepAudioController>();
        if (type == ENEMYTYPE.flying)
        {
            footstep_controller.SetupSound(gameObject, FOOTSTEP.bat);
        }
        else if (type == ENEMYTYPE.ground)
        {
            footstep_controller.SetupSound(gameObject, FOOTSTEP.spider);
        }
        else if (type == ENEMYTYPE.ranged)
        {
            footstep_controller.SetupSound(gameObject, FOOTSTEP.pyro);
        }
    }

    bool IsMoving()
    {
        if (agent.velocity.x == 0 && agent.velocity.y == 0 && agent.velocity.z == 0)
        {
            return false;
        }
        return true;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
            pl_movement = player.GetComponent<Movement>();
        }
        if (IsMoving())
        {
            if (!footstep_controller.IsPlaying(gameObject))
            {
                footstep_controller.PlaySound(gameObject);
            }
        }
        else
        {
            if (footstep_controller.IsPlaying(gameObject))
            {
                footstep_controller.StopSound(gameObject);
            }
        }

        if (Input.GetKey("e"))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKey("f"))
        {
            Time.timeScale = 1;
        }
        if (player)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }

        hear_volume = (pl_movement.FootStepVolume() * 20) - distance;
        Movement();   
    }

    bool InLineOfSight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 100, sight_layer_mask))
        {         
            if (hit.transform.gameObject.tag == "Player")
            {       
                Debug.Log("LINE OF SIGHT");
                return true;
            }
        }
        return false;
    }

    void Movement()
    {       
        // If player is in range, start chasing
        if ((distance <= detect_range) && player)
        {
            if (type == ENEMYTYPE.ground || type == ENEMYTYPE.flying)
            {
                current_state = STATE.chase;
            }
            else if (type == ENEMYTYPE.ranged)
            {
                if (InLineOfSight())
                {
                    Debug.Log("TRUE");
                    current_state = STATE.fire;
                }
                else
                {
                    current_state = STATE.chase;
                }
            }
        }
        
        // If chasing player and goes out of range, start searching
        if (current_state == STATE.chase)
        {
            if (player != null)
            {
                ChasePlayer();
            }
            else
            {
                //current_state = STATE.patrol;
            }

            if (distance > detect_range)
            {
                if (SearchDelay())
                {
                    current_state = STATE.search;
                }
            }

            if (type == ENEMYTYPE.ranged && InLineOfSight())
            {
                current_state = STATE.fire;
            }
        }
        else if (current_state == STATE.fire)
        {
            if (distance > detect_range)
            {
                if (SearchDelay())
                {
                    StartCoroutine(SwitchDelay(STATE.patrol, 2.0f));
                }
            }
            else
            {
                Fire();
                LookatSmoothly(player.transform.position);
            }
        }
        else if (current_state == STATE.patrol)
        {
            FollowPath();
        }
        else if (current_state == STATE.search)
        {
            RandomMovement();          
        }
    }

    bool SearchDelay()
    {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            if (hear_volume > detect_volume)
            {
                return false;
            }
        }
        Debug.Log("TRUUUUUE");
        return true;
    }

    void Fire()
    {
        EnemyFire fireball = GetComponent<EnemyFire>();
        fireball.Fire(player, audio_controller);
        anim.SetBool("Idle", true);
        anim.SetBool("Moving", false);
        agent.speed = 0;
    }

    IEnumerator SwitchDelay(STATE state, float delay)
    {
        yield return new WaitForSeconds(delay);
        current_state = state;
    }

    void ChasePlayer()
    {       
        LookatSmoothly(agent.steeringTarget);
        agent.speed = chase_speed;
        anim.SetFloat("Speed", 1.5f);
        searching = false;
        if (distance > hit_range)
        {
            agent.speed = chase_speed;
            anim.SetBool("Attack", false);
            anim.SetBool("Moving", true);
            anim.SetBool("Idle", false);
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.speed = 0;
            anim.SetBool("Attack", true);
            anim.SetBool("Moving", false);
            anim.SetBool("Idle", true);
            Health pl_health = player.GetComponent<Health>();
            if (pl_health.DealDamage(damage) && audio_controller != null)
            {
                audio_controller.PlaySound(gameObject, SOUND.attack);
            }
            
        }
    }

    void FollowPath()
    {
        LookatSmoothly(agent.steeringTarget);
        anim.SetBool("Attack", false);
        anim.SetBool("Moving", true);
        anim.SetBool("Idle", false);
        agent.speed = patrol_speed;
        anim.SetFloat("Speed", 1.0f);
        searching = false;
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
            agent.SetDestination(path_points[path_index].position);
        }
    }

    void RandomMovement()
    {
        LookatSmoothly(agent.steeringTarget);
        anim.SetBool("Attack", false);
        anim.SetBool("Moving", true);
        anim.SetBool("Idle", false);
        agent.speed = search_speed;
        anim.SetFloat("Speed", 0.7f);
        if (!searching)
        {
            StartCoroutine(SearchTimer(10));
        }

        if (Vector3.Distance(transform.position, random_pos) < 2)
        {
            chase_played = false;
            StartCoroutine(GetRandomPos());
        }
        agent.SetDestination(random_pos);
    }
    

    IEnumerator GetRandomPos()
    {
        // Gets random position within the navmesh
        Vector3 randomDirection = Random.insideUnitSphere * search_radius;
        randomDirection += player.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, search_radius, 1);
        random_pos = hit.position;
        yield return null;
    }

    

    IEnumerator SearchTimer(float time)
    {        
        searching = true;        
        StartCoroutine(GetRandomPos());
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }        
        if (current_state == STATE.search)
        {
            // Finds closest patrol point after losing the player
            float lowest_distance = 100;
            for (int i = 0; i < path_points.Count; i++)
            {
                if (Vector3.Distance(path_points[i].position, transform.position) < lowest_distance)
                {
                    lowest_distance = Vector3.Distance(path_points[i].position, transform.position);
                    path_index = i;
                }
            }
            current_state = STATE.patrol;
        }
    }

    void LookatSmoothly(Vector3 target)
    {
        var target_rotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, turn_speed * Time.deltaTime);
    }
}