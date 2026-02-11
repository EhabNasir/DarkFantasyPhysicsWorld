using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ConstraintSolver : MonoBehaviour
{
    [SerializeField] List<PointsConstraint> constriants;
    [SerializeField] List<FollowerConstraint> F_constriants;
    [SerializeField] int iterationsPerFrame = 6;

    void Update()
    {
        //ConstraintStep();
    }

    void SolveConstraints(PointsConstraint c)
    {
        Vector2 delta = c.pointB.position - c.pointA.position;

        if(delta.magnitude > 0)
        {
            float distance = delta.magnitude;

            float difference = (distance - c.restLength) / distance;

            Vector2 offset = delta * difference * c.springCoefficient;

            float totalWeight = c.pointA.inverseMass + c.pointB.inverseMass;

            c.pointA.position += offset * (c.pointA.inverseMass / totalWeight);
            c.pointB.position -= offset * (c.pointB.inverseMass / totalWeight);
        }
    }

    void SolveFollowerConstraints(FollowerConstraint f)
    {
        Vector2 delta = f.pointFollower.position - f.pointLeader.position;
        float deltaMag = delta.magnitude;

        if(deltaMag > 0)
        {
            float difference = (deltaMag / f.restLength) / deltaMag;

            Vector2 offset = delta * difference * f.springCoefficient;

            float totalWeight = f.pointLeader.inverseMass + f.pointFollower.inverseMass;

            f.pointFollower.position -= offset;
        }
    }

    public void ConstraintStep()
    {
        foreach(PointsConstraint c in constriants)
            SolveConstraints(c);

        foreach (FollowerConstraint f in F_constriants)
            SolveFollowerConstraints(f);
    }

}
