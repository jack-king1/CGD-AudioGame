using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enums;

public class ChestOpen : MonoBehaviour
{

    public Animator anim;
    public bool animPlayed = false;
    public GameObject ps;
    public GameObject button;

    private int playerID;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerID = GetComponent<PlayerData>().PlayerID();
    }

   
    private void OnTriggerStay(Collider other)
    {
        //show b button
        if (!animPlayed)
        {
            button.SetActive(true);
        }

        if (other.gameObject.tag == "Player" && InputManager.BButton(playerID))
        {
            if (!animPlayed)
            {
                
                StartCoroutine (Chest());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            button.SetActive(false);
        }
    }

    IEnumerator Chest()
    {
        anim.Play("Chest Open");
        animPlayed = true;
        button.SetActive(false);
        yield return new WaitForSeconds(2.1f);
        ps.SetActive(true);
    }
}
