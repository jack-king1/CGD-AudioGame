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


    public float maxTime =60;
    public float timer;
    public GameObject Player;
    public Light lamp;

    public float darkness;
    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            m_Player = Player.transform;
            lamp = Player.GetComponentInChildren<Light>();
        }

        timer += Time.deltaTime ;
        if(timer> maxTime)
        {
            m_radius -= 0.5f;
            if(lamp)
            {
                lamp.spotAngle -= 10;
                lamp.color -= (Color.white / 7.0f);
            }
            else
            {
                Debug.LogError("No Lamp Model Specified you mongrel.");
            }
            timer = 0;
            
        }
        if(levelManager)
        {
            if (m_radius <= 3 || lamp.spotAngle == 0)

            {
                levelManager.LoseScene();
            }
        }
        else
        {
            Debug.LogError("Add the level manager prefab, mongrel.");
        }

        if(m_FogOfWarPlane)
        {
            Ray r = new Ray(transform.position, m_Player.position - transform.position);
            if (Physics.Raycast(r, out RaycastHit hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
            {
                for (int i = 0; i < m_vertices.Length; i++)
                {
                    Vector3 v = m_FogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                    float dist = Vector3.SqrMagnitude(v - hit.point);

                    if (dist < m_radiusSqr)
                    {
                        float alpha = Mathf.Min(m_colours[i].a, dist / m_radiusSqr);

                        //Just check cam state is not in any other state e.g. cinematic.
                        if (gameObject.GetComponent<CameraFollow>().m_cameraState == enums.CAMERASTATE.follow)
                        {
                            m_verticeDiscovered[i] = true;
                        }
                        m_colours[i].a = alpha;
                    }
                    else if (m_verticeDiscovered[i])
                    {
                        m_colours[i].a = darkness;
                    }
                    else
                    {
                        m_colours[i].a = 1f;

                    }
                }

                UpdateColour();
            }
        }
        else
        {
            Debug.LogError("No Fog OF War Plane, Please Add.");
        }

    }

    void Init()
    {
        m_FogOfWarPlane = GameObject.FindGameObjectWithTag("Fog");
        m_mesh = m_FogOfWarPlane.GetComponent<MeshFilter>().mesh;
        if(m_FogOfWarPlane)
        {
            m_vertices = m_mesh.vertices;
            m_colours = new Color[m_vertices.Length];

            for (int i = 0; i < m_colours.Length; i++)
            {
                m_colours[i] = Color.black;

            }

            UpdateColour();

            foreach (Vector3 vertice in m_vertices)
            {
                m_verticeDiscovered.Add(false);
            }
        }
        else
        {
            Debug.LogError("No Fog Plane Found! Add one you mongrel!");
        }

    }

   void UpdateColour()
    {
        m_mesh.colors = m_colours;
    }

}
