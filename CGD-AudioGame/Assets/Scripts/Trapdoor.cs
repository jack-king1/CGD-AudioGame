using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trapdoor : MonoBehaviour
{
    public Animator anim;
    public bool animPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!animPlayed)
            {
                StartCoroutine(LevelEnd());
                //Potentially add a proper cinematic here later


            }

            else if(animPlayed)

            {
                
            }

        }
    }

    IEnumerator LevelEnd()
    {
        anim.Play("TrapdoorOpen");
        yield return new WaitForSeconds(4.5f);
        animPlayed = true;
        SceneManager.LoadScene("Level2");
    }
}
