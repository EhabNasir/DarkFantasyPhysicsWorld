using UnityEngine;

public class FollowerConstraint : MonoBehaviour
{
    public PointMass pointLeader;
    public PointMass pointFollower;

    public float restLength;
    public float springCoefficient = 0.5f;
}
