using UnityEngine;

class NodeRecord
{
    public NavigationNode Node;
    public NodeRecord Parent;

    public float GCost; // distance from start
    public float HCost; // heuristic
    public float FCost => GCost + HCost;
}
