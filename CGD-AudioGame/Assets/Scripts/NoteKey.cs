using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteKey : MonoBehaviour
{
    public Animator anim;
    public bool masterPlate = false;

    DrawbridgeLock drawbridgeLock;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!masterPlate)
            {
                anim.Play("PuzzleKeyDown");
                drawbridgeLock = transform.gameObject.GetComponentInParent<DrawbridgeLock>();
                string value = transform.name;
                drawbridgeLock.SetValue(value);
                Debug.LogWarning("Note Key Value: " + value);
                //play sound
            }
            if (masterPlate)
            {
                anim.Play("MasterKeyDown");
                //Debug.Log("" + drawbridgeLock.code);

                //play sounds for the code here
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !masterPlate)
        {
            anim.Play("PuzzleKeyUp");

        }

        else if (other.gameObject.tag == "Player" && masterPlate)
        {
            anim.Play("MasterKeyUp");
        }


    }
}
