using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject m_target;
    [Range(0.01f,1.0f)]
    public float m_cameraSpeed;

    [Range(1f, 20f)]
    public float m_cameraZoomOffset = 5f;

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
                m_target.transform.position.z - 2.5f);

            transform.position = Vector3.Slerp(transform.position, followPosition, m_cameraSpeed);
        }
        else
        {
            Debug.LogError("You absolute mongrel! Camera can't find gameobject tagged with 'Player'. Please specify a target via" +
                " the script on the camera. https://www.youtube.com/watch?v=S8rRladhM1g ");
        }
       
    }
}
