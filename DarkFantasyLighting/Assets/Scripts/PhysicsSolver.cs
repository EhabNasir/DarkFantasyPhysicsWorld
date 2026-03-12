using System.Collections.Generic;
using UnityEngine;

public class PhysicsSolver : MonoBehaviour
{
    public List<PointMass> _points;
    public List<PointCollider> _colliders;
    public ConstraintSolver _constraintSolver;
    public TrapezoidConstraint _blobConstraint;
    public CCD_IK_Solver _IK_Solver;
    public TargetPoseController _targetPoseController;

    // Update is called once per frame
    void FixedUpdate()
    {
        // 1. APPLY FORCES (before integration!)
        if (_targetPoseController)
        {
            _targetPoseController.ApplyTargetPose();
        }

        foreach (var point in _points)
        {
            point.VerletStep();
        }

        for (int i = 0; i < 4; i++)
        {
            foreach (var collider in _colliders)
            {
                collider.ResolveSurfaceCollision();
            }

            if(_constraintSolver)
                _constraintSolver.ConstraintStep();

            if(_blobConstraint)
                _blobConstraint.CalculateArea();

            if(_IK_Solver)
                _IK_Solver.CCDSolver();
        }
    }
}
