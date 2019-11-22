using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject m_target;
    [Range(0.1f,1.0f)]
    public float m_cameraSpeed;

    [Range(1f, 10f)]
    public float m_cameraZoomOffset;

    private void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(m_target)
        {
            Vector3 followPosition = new Vector3(
                m_target.transform.position.x, 
                m_target.transform.position.y + m_cameraZoomOffset,
                m_target.transform.position.z);

            transform.position = Vector3.Lerp(transform.position, followPosition, 0.5f);
        }
        else
        {
            Debug.LogError("You absolute mongrel! Camera can't find gameobject tagged with 'Player'. Please specify a target via" +
                " the script on the camera. https://www.youtube.com/watch?v=S8rRladhM1g ");
        }
       
    }
}
