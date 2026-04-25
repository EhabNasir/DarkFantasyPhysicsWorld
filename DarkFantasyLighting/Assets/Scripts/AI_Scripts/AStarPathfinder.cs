using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder
{        
    float Heuristic(NavigationNode a, NavigationNode b)
        {
            return Vector2.Distance(a.transform.position, b.transform.position);
        }

    NodeRecord GetLowestFCost(List<NodeRecord> list)
    {
        NodeRecord best = list[0];

        foreach (var node in list)
        {
            if (node.FCost < best.FCost)
                best = node;
        }

        return best;
    }

    List<NavigationNode> ReconstructPath(NodeRecord endNode)
    {
        List<NavigationNode> path = new List<NavigationNode>();

        NodeRecord current = endNode;

        while (current != null)
        {
            path.Add(current.Node);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

    public List<NavigationNode> FindPath(NavigationNode start, NavigationNode goal)
    {
        List<NodeRecord> openList = new List<NodeRecord>();
        HashSet<NavigationNode> closedSet = new HashSet<NavigationNode>();

        NodeRecord startRecord = new NodeRecord
        {
            Node = start,
            GCost = 0,
            HCost = Heuristic(start, goal)
        };



        openList.Add(startRecord);

        while (openList.Count > 0)
        {
            NodeRecord current = GetLowestFCost(openList);

            if (current.Node == goal)
                return ReconstructPath(current);

            openList.Remove(current);
            closedSet.Add(current.Node);

            foreach (var connection in current.Node.Connections)
            {
                if (connection.Target == null) continue;

                NavigationNode neighbour = connection.Target;

                if (closedSet.Contains(neighbour))
                    continue;

                float newGCost = current.GCost + connection.Cost;

                NodeRecord neighbourRecord = openList.Find(n => n.Node == neighbour);

                if (neighbourRecord == null)
                {
                    neighbourRecord = new NodeRecord
                    {
                        Node = neighbour,
                        Parent = current,
                        GCost = newGCost,
                        HCost = Heuristic(neighbour, goal)
                    };

                    openList.Add(neighbourRecord);
                }
                else if (newGCost < neighbourRecord.GCost)
                {
                    neighbourRecord.GCost = newGCost;
                    neighbourRecord.Parent = current;
                }
            }
        }

        return null; // no path found
    }
}