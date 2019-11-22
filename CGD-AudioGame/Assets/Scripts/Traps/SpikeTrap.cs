using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 5;
    public float timer = 2.0f;
    public bool raised = false;
    public List<GameObject> targets = new List<GameObject>();
    public float speed = 10;
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Raise());
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, target, step);
        if (raised)
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
        target = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        raised = true;
        yield return new WaitForSeconds(timer);
        StartCoroutine(Lower());
    }

    IEnumerator Lower()
    {
        target = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        raised = false;
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
