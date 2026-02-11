using UnityEngine;

public class PointsConstraint : MonoBehaviour
{
    public PointMass pointA;
    public PointMass pointB;

    public float restLength;
    public float springCoefficient = 0.5f;
}