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
        if (this.tag != "Enemy")
        {


            if (col.tag == "Player")
            {
                Pd.playerScore += 1.0f;


                Destroy(this.gameObject);
            }
        }

        if (col.tag == "CullRange")
        {
            Debug.Log("????");
            Mesh.enabled = true;

        }

    }
    private void OnTriggerExit(Collider col)
    {
        Mesh.enabled = false;
    }

}
