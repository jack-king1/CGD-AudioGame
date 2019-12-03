using UnityEngine;
using System.Collections;

public class DoorKey : MonoBehaviour
{

    public bool inTrigger;
    public float speed = 10f;

    public GameObject key;
    public GameObject button;


    public bool starterKey = false;
    public bool Key1 = false;
    public bool Key2 = false;
    public bool Key3 = false;

    private int playerID;

    private void Start()
    {
        playerID = GetComponent<PlayerData>().PlayerID();
    }

    private void OnTriggerEnter(Collider other)
    {
            button.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && starterKey && InputManager.BButton(playerID))
        {
            
            KeyManager.playerHasStarterKey = true;
            key.SetActive(false);
            button.SetActive(false);
        }

        if (other.gameObject.tag == "Player" && Key1 && InputManager.BButton(playerID))
        {

            KeyManager.playerHasKey1 = true;
            key.SetActive(false);
            button.SetActive(false);
        }

        if (other.gameObject.tag == "Player" && Key2 && InputManager.BButton(playerID))
        {
            KeyManager.playerHasKey2 = true;
            key.SetActive(false);
            button.SetActive(false);
        }

        if (other.gameObject.tag == "Player" && Key3 && InputManager.BButton(playerID))
        {
            KeyManager.playerHasKey3 = true;
            key.SetActive(false);
            button.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        button.SetActive(false);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}