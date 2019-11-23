using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GameObject grid;
    GameObject player;
    Grid grid_scr;
    public Transform target;
    List<Node> follow_path = new List<Node>();

    private void Awake()
    {
        grid = GameObject.FindWithTag("Grid");
        player = GameObject.FindWithTag("Player");
        grid_scr = grid.GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(transform.position, target.position);
    }

    public void SetTarget(Transform t_target)
    {
        target = t_target;
    }

    void FindPath(Vector3 start_pos, Vector3 target_pos)
    {
        Node start_node = grid_scr.NodeFromWorldPoint(start_pos);
        Node target_node = grid_scr.NodeFromWorldPoint(target_pos);

        List<Node> open_list = new List<Node>();
        HashSet<Node> closed_list = new HashSet<Node>();

        open_list.Add(start_node);

        while(open_list.Count > 0)
        {
            Node this_node = open_list[0];
            for(int i = 1; i < open_list.Count; i++)
            {
                if (open_list[i].f_cost < this_node.f_cost || open_list[i].f_cost == this_node.f_cost && open_list[i].h_cost < this_node.h_cost)
                {
                    this_node = open_list[i];
                }
            }
            open_list.Remove(this_node);
            closed_list.Add(this_node);

            if (this_node == target_node)
            {
                GetFinalPath(start_node, target_node);
            }

            foreach (Node neighbor in grid_scr.GetNeighboringNodes(this_node))
            {
                if (!neighbor.is_wall || closed_list.Contains(neighbor))
                {
                    continue;
                }
                int MoveCost = this_node.g_cost + GetManhattenDistance(this_node, neighbor);

                if (MoveCost < neighbor.g_cost || !open_list.Contains(neighbor))
                {
                    neighbor.g_cost = MoveCost;
                    neighbor.h_cost = GetManhattenDistance(neighbor, target_node);
                    neighbor.parent_node = this_node;

                    if(!open_list.Contains(neighbor))
                    {
                        open_list.Add(neighbor);
                    }
                }
            }

        }
    }

    void GetFinalPath(Node first_node, Node last_node)
    {
        List<Node> final_path = new List<Node>();
        Node CurrentNode = last_node;

        while(CurrentNode != first_node)
        {
            final_path.Add(CurrentNode);
            CurrentNode = CurrentNode.parent_node;
        }

        final_path.Reverse();
        follow_path = final_path;
        grid_scr.path = final_path;

    }

    public Vector3 FirstNode()
    {
        if (follow_path.Count > 0)
        {
            return follow_path[0].position;
        }
        else
        {
            Debug.Log("THIS");
            return new Vector3(0, 0, 0);
        }
    }

    int GetManhattenDistance(Node node_a, Node node_b)
    {
        int ix = Mathf.Abs(node_a.grid_x - node_b.grid_x);
        int iy = Mathf.Abs(node_a.grid_y - node_b.grid_y);

        return ix + iy;
    }
}
