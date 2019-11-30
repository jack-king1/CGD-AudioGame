using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public FogOfWarScript fow;
    private PlayerData Pd;
    public MeshRenderer Mesh;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Coint Collided");
        if (col.CompareTag("Player"))
        {
            //Add player score here bois.
            //Pd.playerScore += 1.0f;

            Destroy(gameObject);
        }

        if (col.tag == "CullRange")
        {
            //use distance to player here rather than collding. 
            // Put it in update or something ex. Vector.Distance(sutff) on google :)
            Debug.Log("????");
            Mesh.enabled = true;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        //Mesh.enabled = false;
    }

}
