using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class ChestOpen : MonoBehaviour
{

    public Animator anim;
    public bool animPlayed = false;
    public GameObject ps;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!animPlayed)
            {
                StartCoroutine (Chest());
            }
        }
    }

    IEnumerator Chest()
    {
        anim.Play("Chest Open");
        yield return new WaitForSeconds(2.1f);
        ps.SetActive(true);
        animPlayed = true;
    }
}
