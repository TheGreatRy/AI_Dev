using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

public class NavAStar : MonoBehaviour
{
    public static bool Generate(NavNode start, NavNode end, ref List<NavNode> path)
    {
        var nodes = new SimplePriorityQueue<NavNode>();

        start.Cost = 0;
        float heuristic = (start.transform.position - end.transform.position).magnitude;
        nodes.Enqueue(start, start.Cost + heuristic);

        bool found = false;
        while (nodes.Count > 0 && !found)
        {
            var currentNode = nodes.Dequeue();
            if (currentNode == end)
            {
                found = true;
                break;
            }
            foreach (var neighbor in currentNode.neighbors)
            {
                float cost = currentNode.Cost + (currentNode.transform.position - neighbor.transform.position).magnitude;
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost;
                    neighbor.Previous = currentNode;

                    heuristic = (neighbor.transform.position - end.transform.position).magnitude;

                    nodes.Enqueue(neighbor, cost);
                }
            }

        }
        if (found)
        {
            NavNode.CreatePath(end, ref path);

        }
        return found;
    }
}
