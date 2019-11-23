using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int grid_x;
    public int grid_y;

    public bool is_wall;
    public Vector3 position;

    public Node parent_node;

    public int g_cost;
    public int h_cost;

    public int f_cost { get { return g_cost + h_cost; } }

    public Node(bool wall, Vector3 pos, int x, int y)
    {
        is_wall = wall;
        position = pos;
        grid_x = x;
        grid_y = y;
    }

}
