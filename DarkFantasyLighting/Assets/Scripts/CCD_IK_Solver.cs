using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using NUnit.Framework.Constraints;

public class CCD_IK_Solver : MonoBehaviour
{
    [SerializeField] GameObject target;

    [Space(10)]

    [SerializeField] Vector2 targetPosition;
    [SerializeField] PointMass endEffector;
    [SerializeField] PointMass[] joints;

    private void Update()
    {
        targetPosition = target.transform.position;
    }

    public void CCDSolver()
    {
        endEffector = joints[joints.Length - 1];

        if (Vector2.Distance(endEffector.position, targetPosition) < 0.01f)
            return;

        //2. get vector current joint to end effector
        for(int i = joints.Length - 2; i >= 0; i--)
        {

            //1. get vector from end -> target
            Vector2 r1 = targetPosition - joints[i].position;

            Vector2 r2 = endEffector.position - joints[i].position;

            float cross = (r2.x * r1.y) - (r2.y * r1.x); // for direction
            float dot = Vector2.Dot(r2, r1); // for angle

            float angle = Mathf.Atan2(cross,dot); // combine for signed angle

            angle = Mathf.Clamp(angle, -0.1f, 0.1f); // clamp angle for small steps, dont apply entire correction at once

            // loop though all joints above and rotate
            for(int j = i + 1; j < joints.Length; j++)
            {
                //convert joint to local space, for new origin
                Vector2 localPoint = joints[j].position - joints[i].position;

                // rotate point around joint origin
                float rotatedPointX = Mathf.Cos(angle) * localPoint.x - Mathf.Sin(angle) * localPoint.y;
                float rotatedPointY = Mathf.Sin(angle) * localPoint.x + Mathf.Cos(angle) * localPoint.y;

                // convert back into world space
                Vector2 worldPoint = joints[i].position + new Vector2(rotatedPointX, rotatedPointY);

                // update point position
                joints[j].position = worldPoint;
            }
        }
    }
}
