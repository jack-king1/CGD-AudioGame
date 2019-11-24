using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingPongScale : MonoBehaviour
{
    [Range(0.1f, 5)]
    [SerializeField] float min_scale_x = 0.8f;
    [Range(0.1f, 5)]
    [SerializeField] float min_scale_y = 0.8f;
    [Range(0.1f, 5)]
    [SerializeField] float min_scale_z = 0.8f;
    [Range(0.1f, 5)]
    [SerializeField] float max_scale_x = 1.2f;
    [Range(0.1f, 5)]
    [SerializeField] float max_scale_y = 1.2f;
    [Range(0.1f, 5)]
    [SerializeField] float max_scale_z = 1.2f;
    [Range(0.1f, 2)]
    [SerializeField] float speed = 0.2f;

    private void Update()
    {
        //transform.localScale = new Vector3(Mathf.PingPong(Time.time + min_scale_x, max_scale_x), 
        //    Mathf.PingPong(Time.time + min_scale_y, max_scale_y), Mathf.PingPong(Time.time + min_scale_z, max_scale_z));

        transform.localScale = new Vector3(Mathf.PingPong(Time.time * speed, max_scale_x - 1) + min_scale_x, Mathf.PingPong(Time.time * speed, max_scale_y - 1) + min_scale_y, 0);
    }
}
