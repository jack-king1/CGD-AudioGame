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

    // Start is called before the first frame update
    // posistion -60, -90 -120
    void Start()
    {
        anim = GetComponent<Animator>();
        playerID = GetComponent<PlayerData>().PlayerID();

        coin1.SetActive(false);
        coin2.SetActive(false);
        coin3.SetActive(false);
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
                //coin1.GetComponent<GameObject>().transform.rotation.y(-90);
                coin1 = Instantiate(coin1, transform.TransformPoint(Vector3.forward), transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,-90,0)));


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
        coin1.SetActive(true);
        coin2.SetActive(true);
        coin3.SetActive(true);
        anim.Play("CoinBounce");
    }
}
