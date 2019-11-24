using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{
    public GameObject m_FogOfWarPlane;
    public Transform m_Player;
    public LayerMask m_fogLayer;
    public float m_radius = 5.0f;
    private  float  m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    public List<bool> m_verticeDiscovered;
    private Color[] m_colours;


    public float maxTime =10;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime ;
        if(timer> maxTime)
        {
            m_radius -= 0.5f;
            timer = 0;

        }

       if(m_radius <= 2.5)
        {
            //Player.SetActive(false);
        }

        Ray r = new Ray(transform.position, m_Player.position - transform.position);
        RaycastHit hit;
        if(Physics.Raycast(r,out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {
            for(int i=0; i<m_vertices.Length; i++)
            {
                Vector3 v = m_FogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);

                if(dist< m_radiusSqr)
                {
                    float alpha = Mathf.Min(m_colours[i].a, dist / m_radiusSqr);

                    //Just check cam state is not in any other state e.g. cinematic.
                    if(gameObject.GetComponent<CameraFollow>().m_cameraState == enums.CAMERASTATE.follow)
                    {
                        m_verticeDiscovered[i] = true;
                    }
                    m_colours[i].a = alpha;
                }
                else if(m_verticeDiscovered[i])
                {
                    m_colours[i].a = 0.5f;
                }
                else
                {
                    m_colours[i].a = 1f;

                }
            }

            UpdateColour();
        }
    }

    void Init()
    {
        m_mesh = m_FogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colours = new Color[m_vertices.Length];

        for(int i=0; i< m_colours.Length; i++)
        {
            m_colours[i] = Color.black;

        }

        UpdateColour();

        foreach (Vector3 vertice in m_vertices)
        {
            m_verticeDiscovered.Add(false);
        }
    }

   void UpdateColour()
    {
        m_mesh.colors = m_colours;
    }

}
