using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;
public class SpikeTrap : MonoBehaviour
{
    public int damage = 5;
    public float timer = 6.0f;
    public float alternateTimer = 2.0f;
    public float damageWindow = 0.25f;
    public bool isUndelayedTrap = false;
    public bool raised = false;
    public bool canDealDamage = false;
    public List<GameObject> targets = new List<GameObject>();
    public float speed = 30;
    private Vector3 target;
    TrapAudioController audio_controller;

    private bool initialOffsetComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Raise());
        if (raised)
        {
            StartCoroutine(Lower());
        }
        else
        {
            StartCoroutine(Raise());
        }
        if (isUndelayedTrap)
        {
            alternateTimer = 6.0f;
        }

        audio_controller = GameObject.Find("AudioController").GetComponent<TrapAudioController>();
        if (audio_controller != null)
        {
            audio_controller.SetupSound(gameObject, TRAP.spike);
        }
        else
        {
            Debug.Log("Audio controller not setup");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, target, step);
        if (canDealDamage)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null)
                {
                    Health health = targets[i].GetComponent<Health>();
                    health.DealDamage(damage);
                }
            }
        }
    }

    IEnumerator Raise()
    {    
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        if (!initialOffsetComplete && !isUndelayedTrap)
        {
            yield return new WaitForSeconds(timer / 1.5f);
            initialOffsetComplete = true;
            Debug.Log("Offset waited");
        }
        canDealDamage = true;
        while (transform.GetChild(0).position != target)
        {
            yield return null;
        }
        if (audio_controller != null)
        {
            audio_controller.SetParameter(gameObject, "Direction", 1.0f);
            audio_controller.PlaySound(TRAP.spike, gameObject);
        }
        yield return new WaitForSeconds(damageWindow);
        canDealDamage = false;
        raised = true;
        yield return new WaitForSeconds(timer/3.0f - damageWindow);
        StartCoroutine(Lower());
    }

    IEnumerator Lower()
    {
        canDealDamage = false;
        target = new Vector3(transform.position.x, transform.position.y - 3.0f, transform.position.z);
        
        raised = false;
        while (transform.GetChild(0).position != target)
        {
            yield return null;
        }
        if (audio_controller != null)
        {
            audio_controller.SetParameter(gameObject, "Direction", 0.0f);
            audio_controller.PlaySound(TRAP.spike, gameObject);
        }
        yield return new WaitForSeconds(timer);
        StartCoroutine(Raise());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER");
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            targets.Remove(other.gameObject);
        }
    }
}
