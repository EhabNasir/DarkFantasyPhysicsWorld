using System.Collections.Generic;
using UnityEngine;

public class PathTest : MonoBehaviour
{
    public NavigationNode start;
    public NavigationNode end;

    void Start()
    {
        AStarPathfinder pathfinder = new AStarPathfinder();
        List<NavigationNode> path = pathfinder.FindPath(start, end);

        if (path == null)
        {
            Debug.Log("No path found");
            return;
        }

        foreach (var node in path)
        {
            Debug.Log("Path node: " + node.name);
        }
    }
}
