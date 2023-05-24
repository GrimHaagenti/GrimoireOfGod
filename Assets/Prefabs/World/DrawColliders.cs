using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DrawColliders : MonoBehaviour
{
    [SerializeField] Color color = Color.green;
    void OnDrawGizmos()
    {
        //GetComponentsInChildren also returns components on parent
        Collider collider = GetComponent<Collider>();

        Gizmos.color = color;

            var points = GetBoundsPoints(collider.bounds);
            DrawPoints(points);
    }

    Vector3[] GetBoundsPoints(Bounds bounds)
    {

        Vector3[] points = {
            bounds.min,
            new Vector3(bounds.max.x,bounds.min.y, bounds.min.z),
            new Vector3(bounds.min.x,bounds.max.y, bounds.min.z),
            new Vector3(bounds.min.x,bounds.min.y, bounds.max.z),
            new Vector3(bounds.max.x,bounds.max.y, bounds.min.z),
            new Vector3(bounds.min.x,bounds.max.y, bounds.max.z),
            new Vector3(bounds.max.x,bounds.min.y, bounds.max.z),
            bounds.max
        };
        return points;
    }


    void DrawPoints(Vector3[] points)
    {
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[0], points[2]);
        Gizmos.DrawLine(points[0], points[3]);
        Gizmos.DrawLine(points[1], points[4]);
        Gizmos.DrawLine(points[2], points[5]);
        Gizmos.DrawLine(points[3], points[5]);
        Gizmos.DrawLine(points[4], points[7]);
        Gizmos.DrawLine(points[5], points[7]);
        Gizmos.DrawLine(points[6], points[7]);
        Gizmos.DrawLine(points[1], points[6]);
        Gizmos.DrawLine(points[2], points[4]);
        Gizmos.DrawLine(points[3], points[6]);

    }
}
