using System.Collections.Generic;
using UnityEngine;

public class RoomNavigationGraph : MonoBehaviour
{
    public List<NavigationNode> Nodes = new List<NavigationNode>();

    private void Awake()
    {
        Nodes.AddRange(GetComponentsInChildren<NavigationNode>());
    }

    public NavigationNode GetClosestNode(Vector2 position)
    {
        NavigationNode closest = null;
        float minDist = float.MaxValue;

        foreach (var node in Nodes)
        {
            float dist = Vector2.Distance(position, node.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = node;
            }
        }

        return closest;
    }
}
