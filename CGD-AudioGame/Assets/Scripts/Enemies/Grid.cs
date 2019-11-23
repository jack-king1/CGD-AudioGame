using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform start_pos;
    public LayerMask wall_mask;
    public Vector2 grid_size = new Vector2(30, 30);
    public float node_radius = 0.5f;
    public float node_distance;
    Node[,] grid;
    public List<Node> path;
    float node_diameter;
    int size_x, size_y;

    private void Start()
    {
        node_diameter = node_radius * 2;
        size_x = Mathf.RoundToInt(grid_size.x / node_diameter);
        size_y = Mathf.RoundToInt(grid_size.y / node_diameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[size_x, size_y];
        Vector3 bottom_left = transform.position - Vector3.right * grid_size.x / 2 - Vector3.forward * grid_size.y / 2;
        for (int x = 0; x < size_x; x++)
        {
            for (int y = 0; y < size_x; y++)
            {
                Vector3 worldPoint = bottom_left + Vector3.right * (x * node_diameter + node_radius) + Vector3.forward * (y * node_diameter + node_radius);
                bool Wall = true;

                if (Physics.CheckSphere(worldPoint, node_radius, wall_mask))
                {
                    Wall = false;
                }

                grid[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighbors = new List<Node>();
        int x_check;
        int y_check;

        x_check = node.grid_x + 1;
        y_check = node.grid_y;
        if (CheckNodes(x_check, y_check))
        {
            neighbors.Add(grid[x_check, y_check]);
        }

        x_check = node.grid_x - 1;
        y_check = node.grid_y;
        if (CheckNodes(x_check, y_check))
        {
            neighbors.Add(grid[x_check, y_check]);
        }

        x_check = node.grid_x;
        y_check = node.grid_y + 1;
        if (CheckNodes(x_check, y_check))
        {
            neighbors.Add(grid[x_check, y_check]);
        }

        x_check = node.grid_x;
        y_check = node.grid_y - 1;
        if (CheckNodes(x_check, y_check))
        {
            neighbors.Add(grid[x_check, y_check]);
        }

        return neighbors;
    }

    bool CheckNodes(int x_check, int y_check)
    {
        if (x_check >= 0 && x_check < size_x)
        {
            if (y_check >= 0 && y_check < size_y)
            {
                return true;
            }
        }
        return false;
    }

    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float x_pos = ((a_vWorldPos.x + grid_size.x / 2) / grid_size.x);
        float y_pos = ((a_vWorldPos.z + grid_size.y / 2) / grid_size.y);

        x_pos = Mathf.Clamp01(x_pos);
        y_pos = Mathf.Clamp01(y_pos);

        int ix = Mathf.RoundToInt((size_x - 1) * x_pos);
        int iy = Mathf.RoundToInt((size_y - 1) * y_pos);

        return grid[ix, iy];
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(grid_size.x, 1, grid_size.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                if (n.is_wall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }


                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }

                }


                Gizmos.DrawCube(n.position, Vector3.one * (node_diameter - node_distance));
            }
        }
    }
}
