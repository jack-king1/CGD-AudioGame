using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{

    public Animator anim;
    public bool animPlayed = false;

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
                anim.Play("Chest Open");
                animPlayed = true;
            }
        }
    }
}
