using UnityEngine;

[System.Serializable]
public class NodeConnection
{
    public NavigationNode Target;
    public MovementType MovementType;
    public float Cost = 1f;
}