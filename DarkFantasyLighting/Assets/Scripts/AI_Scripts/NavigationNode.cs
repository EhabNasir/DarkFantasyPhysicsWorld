using UnityEngine;
using System.Collections.Generic;

public enum MovementType
{
    Walk,
    Jump,
    Drop,
    Climb
}

public class NavigationNode : MonoBehaviour
{
    public List<NodeConnection> Connections = new List<NodeConnection>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.15f);

        foreach (var connection in Connections)
        {
            if (connection.Target == null) continue;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, connection.Target.transform.position);
        }
    }
}
