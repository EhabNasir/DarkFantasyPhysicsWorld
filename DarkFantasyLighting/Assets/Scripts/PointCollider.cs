using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    [SerializeField] List<PointMass> m_point;
    Vector2 _surfacePoint; Vector2 _normal;
    private void Start()
    {
        _surfacePoint = transform.position;
        _normal = transform.up;
    }

    private void Update()
    {
        //ResolveSurfaceCollision(transform.position, (Vector2)transform.up);
    }

    public void ResolveSurfaceCollision()
    {
        foreach (var point in m_point)
        {
            Vector2 toPoint = point.position - _surfacePoint;

            float signedDistance = Vector2.Dot(toPoint, _normal);

            if (signedDistance <= point.radius)                           
            {
                float penetration = point.radius - signedDistance;
                point.position += _normal * penetration;

                //Stability with bouncing
                Vector2 v = point.position - point.prevPosition;

                Vector2 vNormal = _normal * Vector2.Dot(v, _normal);
                Vector2 vTangent = v - vNormal;

                point.prevPosition = point.position - vTangent;

                //->USE FOR NO BOUNCE
                //point.prevPosition = point.position;
            }
        }
    }
}
