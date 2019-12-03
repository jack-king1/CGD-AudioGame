using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public static bool playerHasStarterKey;
    public static bool playerHasKey1;
    public static bool playerHasKey2;
    public static bool playerHasKey3;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    private void Update()
    {
        if (playerHasKey1)
        {
            Light1.SetActive(true);
        }
        if (playerHasKey2)
        {
            Light2.SetActive(true);
        }
        if (playerHasKey3)
        {
            Light3.SetActive(true);
        }
    }
}
