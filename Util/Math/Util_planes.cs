using UnityEngine;

partial class Util
{
    /// <summary>
    /// Intersects a ray with a plane. Returns true if there is an intersection, false otherwise.
    /// </summary>
    public static bool RayPlaneIntersection(this Plane plane, in Ray ray, out Vector3 intersectionPoint)
    {
        if (plane.Raycast(ray, out float distance))
        {
            intersectionPoint = ray.GetPoint(distance);
            return true;
        }
        intersectionPoint = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Intersects a segment with a plane. Returns true if there is an intersection within the segment.
    /// </summary>
    public static bool SegmentPlaneIntersection(this Plane plane, in Vector3 start, in Vector3 end, out Vector3 intersectionPoint)
    {
        Ray ray = new Ray(start, end - start);
        float segmentLength = Vector3.Distance(start, end);

        if (plane.Raycast(ray, out float distance) && distance <= segmentLength)
        {
            intersectionPoint = ray.GetPoint(distance);
            return true;
        }

        intersectionPoint = Vector3.zero;
        return false;
    }

    /// <summary>
    /// Intersects two planes. Returns true if they intersect as a line (Ray).
    /// </summary>
    public static bool PlanePlaneIntersection(this Plane p1, in Plane p2, out Ray intersectionLine)
    {
        Vector3 direction = Vector3.Cross(p1.normal, p2.normal);
        float det = direction.sqrMagnitude;

        if (det < 1e-6f)
        {
            intersectionLine = new Ray();
            return false; // Planes are parallel
        }

        Vector3 point = ((Vector3.Cross(direction, p2.normal) * p1.distance) +
                         (Vector3.Cross(p1.normal, direction) * p2.distance)) / det;

        intersectionLine = new Ray(point, direction.normalized);
        return true;
    }

    /// <summary>
    /// Intersects three planes. Returns true if they all intersect at a common point.
    /// </summary>
    public static bool ThreePlaneIntersection(this Plane p1, in Plane p2, in Plane p3, out Vector3 intersectionPoint)
    {
        Vector3 n1 = p1.normal, n2 = p2.normal, n3 = p3.normal;
        float d1 = -p1.distance, d2 = -p2.distance, d3 = -p3.distance;

        Vector3 denom = Vector3.Cross(n1, n2);
        float denomDot = Vector3.Dot(denom, n3);

        if (Mathf.Abs(denomDot) < 1e-6f)
        {
            intersectionPoint = Vector3.zero;
            return false;
        }

        intersectionPoint = (
            (-d1 * Vector3.Cross(n2, n3)) +
            (-d2 * Vector3.Cross(n3, n1)) +
            (-d3 * Vector3.Cross(n1, n2))
        ) / denomDot;

        return true;
    }
}