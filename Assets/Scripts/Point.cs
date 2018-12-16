using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    public List<Point> Connections = new List<Point>();
    public float Radius;
    public int Edges;
    public float EdgeWidth;
    private List<Vector3> EdgePoints = new List<Vector3>();
    public LineRenderer LR;

    private void Start()
    {
        LR = GetComponent<LineRenderer>();
        CreateEdges();
    }

    public void CreateEdges()
    {
        LR.positionCount = Edges;
        for (int i = 0; i < Edges; i++)
        {
            EdgePoints.Add(GetPointByAngle(transform.position, (360 / Edges * i) - (360 / Edges)/2, Radius));
        }
        LR.loop = true;
        LR.startWidth = EdgeWidth;
        LR.endWidth = EdgeWidth;
        LR.SetPositions(EdgePoints.ToArray());
    }

    public Vector3 GetPointByAngle(Vector3 origin, float angle, float distance)
    {
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 p = origin;
        p.x += x;
        p.z += z;
        return p;
    }

}
