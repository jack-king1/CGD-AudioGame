using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteKey : MonoBehaviour
{
    public Animator anim;
    public bool masterPlate = false;
    public GameObject button;

    public DrawbridgeLock drawbridgeLock;
    public DrawbridgeLock CheckLights;

    public Material originalColor;

    void Start()
    {
        anim = GetComponent<Animator>();
        drawbridgeLock = GameObject.FindGameObjectWithTag("CodeLock").GetComponent<DrawbridgeLock>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("" + drawbridgeLock.code);

            if (!masterPlate)
            {
                anim.Play("PuzzleKeyDown");
                drawbridgeLock = transform.gameObject.GetComponentInParent<DrawbridgeLock>();
                string value = transform.name;
                drawbridgeLock.SetValue(value);
                Debug.LogWarning("Note Key Value: " + value);
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);

                //play sound
            }
            if (masterPlate)
            {
                anim.Play("MasterKeyDown");
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
                DrawbridgeLock.placeInCode = 0;
                DrawbridgeLock.attempedCode = "";
                CheckLights.LightReset();

                //play sounds for the code herew
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !masterPlate)
        {
            anim.Play("PuzzleKeyUp");
            gameObject.GetComponent<Renderer>().material = originalColor;

        }

        else if (other.gameObject.tag == "Player" && masterPlate)
        {
            anim.Play("MasterKeyUp");
            gameObject.GetComponent<Renderer>().material = originalColor;
        }


    }
}
