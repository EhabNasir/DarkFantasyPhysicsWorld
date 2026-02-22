using UnityEngine;
using System.Collections.Generic;

public class TrapezoidConstraint : MonoBehaviour
{
    [SerializeField] List<PointMass> points = new List<PointMass>();
    [SerializeField] float restArea = 4;
    [SerializeField] float pressureCoefficient = 0.5f;

    [SerializeField] bool renderLine = false;
    [SerializeField] LineRenderer lr;

    private void Start()
    {
        lr.positionCount = points.Count;
    }

    private void Update()
    {
        if (!renderLine)
            return;

        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    public void CalculateArea()
    {
        int count = points.Count;

        float area = 0;

        //GaussesAreaFormula
        for (int i = 0; i < points.Count; i++)
        {
            int next = (i + 1) % points.Count;
            area += (points[i].position.x * points[next].position.y - points[next].position.x * points[i].position.y);
        }

        area *= 0.5f;

        float error = restArea - area;
        Vector2[] forces = new Vector2[count];
        for (int i = 0; i < points.Count; i++)
        {
            //partial derivatives of Shoelace
            int prev = (i - 1 + count) % count;
            int next = (i + 1) % count;

            float xForce = (pressureCoefficient * error) * (points[next].position.y - points[prev].position.y);
            float yForce = (pressureCoefficient * error) * (points[prev].position.x - points[next].position.x);
            forces[i] = new Vector2(xForce, yForce);

        }

        for (int i = 0; i < points.Count; i++)
        {
            points[i].prevPosition = points[i].position;
            points[i].position += forces[i];
        }
    }
}
