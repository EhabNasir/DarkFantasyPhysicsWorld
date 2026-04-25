using UnityEngine;

public class NodeTester : MonoBehaviour
{
    public RoomNavigationGraph graph;

    void Start()
    {
        NavigationNode closest = graph.GetClosestNode(transform.position);
        Debug.Log("Closest node: " + closest.name);
    }
}
