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
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;

    private int playerID;
    GameAudioController audio_controller;
    PickupAudioController pickup_audio_cont;
    // Start is called before the first frame update
    // posistion -60, -90 -120
    void Start()
    {
        anim = GetComponent<Animator>();
        coin1.SetActive(false);
        coin2.SetActive(false);
        coin3.SetActive(false);
        audio_controller = GameObject.Find("AudioController").GetComponent<GameAudioController>();
        pickup_audio_cont = GameObject.Find("AudioController").GetComponent<PickupAudioController>();
        pickup_audio_cont.SetupSound(gameObject, PICKUP.chest);
        pickup_audio_cont.PlaySound(gameObject);
        playerID = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().PlayerID();     
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
        yield return new WaitForSeconds(0.5f);
        audio_controller.PlayCoinSound(gameObject);
        yield return new WaitForSeconds(1.6f);
        pickup_audio_cont.StopSound(gameObject);
        pickup_audio_cont.RemoveSound(gameObject);
        ps.SetActive(true);
        coin1.SetActive(true);
        coin2.SetActive(true);
        coin3.SetActive(true);
        anim.Play("CoinBounce");
    }
}
