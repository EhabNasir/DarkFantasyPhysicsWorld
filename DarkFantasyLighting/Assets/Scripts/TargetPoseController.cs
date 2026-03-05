using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoseTarget
{
    public PointMass point;
    public Vector2 localOffset;  // Offset from pose center
    [Range(0f, 2f)]
    public float pullStrength = 1f;
}

public class TargetPoseController : MonoBehaviour
{
    [Header("Pose Definition")]
    [SerializeField] List<PoseTarget> poseTargets;

    [Header("Settings")]
    [SerializeField] float globalStrength = 5f;
    [SerializeField] bool useDynamicCenter = true;
    [SerializeField] Transform manualPoseCenter;  // Only used if useDynamicCenter is false

    private Vector2 calculatedCenter;

    public void ApplyTargetPose()
    {
        Vector2 centerPos;

        if (useDynamicCenter)
        {
            // Calculate center of mass from all points
            centerPos = CalculateCenterOfMass();
        }
        else
        {
            // Use manual transform
            centerPos = manualPoseCenter.position;
        }

        calculatedCenter = centerPos;

        // Apply forces to each point toward its target position
        foreach (var target in poseTargets)
        {
            Vector2 targetWorldPos = centerPos + target.localOffset;
            Vector2 toTarget = targetWorldPos - target.point.position;

            // Calculate force (spring-like behavior)
            Vector2 force = toTarget * globalStrength * target.pullStrength;

            // Add to the point's net force (before Verlet integration)
            target.point.AddForce(force);
        }
    }

    private Vector2 CalculateCenterOfMass()
    {
        Vector2 centerOfMass = Vector2.zero;
        float totalMass = 0f;

        foreach (var target in poseTargets)
        {
            centerOfMass += target.point.position * target.point.mass;
            totalMass += target.point.mass;
        }

        if (totalMass > 0.001f)
        {
            centerOfMass /= totalMass;
        }

        return centerOfMass;
    }

    // Visualization for debugging
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || poseTargets == null || poseTargets.Count == 0)
            return;

        // Draw center
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(calculatedCenter, 0.15f);

        // Draw target positions
        foreach (var target in poseTargets)
        {
            if (target.point == null) continue;

            Vector2 targetWorldPos = calculatedCenter + target.localOffset;

            // Draw target position
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetWorldPos, 0.1f);

            // Draw line from current to target
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(target.point.position, targetWorldPos);
        }
    }
}