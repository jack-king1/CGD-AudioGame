using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{

    public GameObject Key;
    public GameObject ps;
    public GameObject GateObject;


    public Animator anim;

    public bool animPlayed = false;
    public bool starterGate;
    public bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
            if (!animPlayed && starterGate && KeyManager.playerHasStarterKey)
            {
                StartCoroutine(Gate());
            }
            else if (!animPlayed && !starterGate && KeyManager.playerHasKey1 && KeyManager.playerHasKey2 && KeyManager.playerHasKey3)
            { 
                StartCoroutine(Gate());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }

    IEnumerator Gate()
    {
        anim.Play("GateOpen");
        ps.SetActive(true);
        animPlayed = true;
        yield return new WaitForSeconds(2f);
        GateObject.SetActive(false);
    }

    void OnGUI()
    {
        if (inTrigger && !animPlayed && starterGate)
        {


            if (starterGate)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height - 50, 200, 25), "The door is locked!");
            }
        }
    }
}