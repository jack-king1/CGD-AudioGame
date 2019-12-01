using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCheckpoint : MonoBehaviour
{
    public GameObject checkpoint;

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
            //Destroy previous checkpoint
            Destroy(checkpoint);
        //Destroy this object
            Destroy(gameObject);
        //doing this will set the new checkpoint to be the next respawn
    }
}
