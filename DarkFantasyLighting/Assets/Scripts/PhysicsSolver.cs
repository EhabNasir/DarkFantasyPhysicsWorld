using System.Collections.Generic;
using UnityEngine;

public class PhysicsSolver : MonoBehaviour
{
    public List<PointMass> _points;
    public List<PointCollider> _colliders;
    public ConstraintSolver _constraintSolver;
    public CCD_IK_Solver _IK_Solver;

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var point in _points)
        {
            point.VerletStep();
        }

        for (int i = 0; i < 7; i++)
        {
            foreach (var collider in _colliders)
            {
                collider.ResolveSurfaceCollision();
            }

            if(_constraintSolver)
                _constraintSolver.ConstraintStep();

            if(_IK_Solver)
                _IK_Solver.CCDSolver();
        }
    }
}
