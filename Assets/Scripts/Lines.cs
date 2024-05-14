using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Lines : MonoBehaviour
{
    [SerializeField] float horizontalAperture = 0;
    [SerializeField] int lineDistance = 0;
    [SerializeField] int linesQty = 0;
    [SerializeField] int horizontalLinesQty = 0;
    [SerializeField] float pointRate = 0;
    private List<Line> lines = new List<Line>();
    public List<Line> LinesList { get { return lines; } }

    private void Awake()
    {
        for (int i = 0; i < horizontalLinesQty; i++)
        {
            Vec3 startPos = new Vec3(transform.position);
            Vec3 endPosForward = new Vec3(transform.forward * lineDistance);
            Vec3 endPosRight = new Vec3(transform.right * horizontalAperture * (-horizontalLinesQty / 2) + i * transform.right * horizontalAperture);

            Vec3 endPos = new Vec3(endPosForward + endPosRight);
            endPos.Normalize();
            lines.Add(new Line(startPos, endPos));
        }

        foreach (Line line in lines)
        {
            for (int i = 0; i < lineDistance / pointRate; i++)
            {
                Vec3 point = line.startPos + line.endPos * pointRate * i;
                //Vec3 point = Vec3.Lerp(line.startPos, line.endPos, i * pointRate);
                line.points.Add(point);
            }
        }
    }
    private void Update()
    {
        UpdateLines();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (Line line in lines)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(line.startPos, line.startPos + (line.endPos * lineDistance));
            Gizmos.color = Color.red;
            for (int i = 0; i < line.points.Count; i++)
            {
                Gizmos.DrawSphere(line.points[i], 0.1f);
            }
        }
    }

    private void UpdateLines()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Vec3 startPos = new Vec3(transform.position);
            Vec3 endPosForward = new Vec3(Camera.main.transform.forward * lineDistance);
            Vec3 endPosRight = new Vec3(Camera.main.transform.right * horizontalAperture * (-horizontalLinesQty / 2) + i * Camera.main.transform.right * horizontalAperture);

            Vec3 endPos = new Vec3(endPosForward + endPosRight);
            lines[i].SetLine(startPos, endPos);

            for (int j = 0; j < lineDistance / pointRate; j++)
            {
                Vec3 point = lines[i].startPos + lines[i].endPos * pointRate * j;
                lines[i].points[j] = point;
            }
        }
    }
}