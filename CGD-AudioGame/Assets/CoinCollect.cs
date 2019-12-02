using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{ 
    public float scoreAmount = 100;
    public Transform start;
    public Transform end;

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);

        transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(start.transform.position.y, end.transform.position.y, Mathf.PingPong(Time.time, 1)), gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Coin Collided");
        if (col.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Score>().playerScore += scoreAmount;
            Destroy(gameObject);
        }

    }

}
