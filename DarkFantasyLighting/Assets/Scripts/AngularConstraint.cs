using System.Security.Cryptography;
using UnityEngine;

public class AngularConstraint : MonoBehaviour
{
    [SerializeField] PointMass pointA;
    [SerializeField] PointMass pointHinge;
    [SerializeField] PointMass pointC;

    [SerializeField] float restAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SolverAngularConstraint()
    {
        Vector2 u = pointA.position - pointHinge.position;
        Vector2 v = pointC.position - pointHinge.position;

        float cosAngle = Vector2.Dot(u, v) / (u.magnitude * v.magnitude);
        float angle = Mathf.Acos(cosAngle);

        float angleError = restAngle - angle;
    }
}
