using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFullscreen : MonoBehaviour
{
    public bool fullScreenActivated; 

    public void FullScreenActivation()
    {
        Screen.fullScreen = fullScreenActivated;

        Debug.Log("SCREEN" + Screen.fullScreen);
    }
}
