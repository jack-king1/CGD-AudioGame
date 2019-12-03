using UnityEngine;
using System.Collections;

public class DoorKey : MonoBehaviour
{

    public bool inTrigger;
    public float speed = 10f;

    public GameObject key;


    public bool starterKey = false;
    public bool Key1 = false;
    public bool Key2 = false;
    public bool Key3 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && starterKey)
        {
            key.SetActive(false);
            KeyManager.playerHasStarterKey = true;
        }

        if (other.gameObject.tag == "Player" && Key1)
        {
            key.SetActive(false);
            KeyManager.playerHasKey1 = true;
        }

        if (other.gameObject.tag == "Player" && Key2)
        {
            key.SetActive(false);
            KeyManager.playerHasKey2 = true;
        }

        if (other.gameObject.tag == "Player" && Key3)
        {
            key.SetActive(false);
            KeyManager.playerHasKey3 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}