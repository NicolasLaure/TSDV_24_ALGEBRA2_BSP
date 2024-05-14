using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Lines : MonoBehaviour
{
    [SerializeField] float horizontalAperture = 0;
    [SerializeField] float verticalAperture = 0;
    [SerializeField] int lineDistance = 0;
    [SerializeField] int horizontalLinesQty = 0;
    [SerializeField] int verticalLinesQty = 0;
    [SerializeField] float pointRate = 0;
    private List<List<Line>> bidimensionalLines = new List<List<Line>>();
    public List<List<Line>> LinesList { get { return bidimensionalLines; } }

    private void Awake()
    {
        for (int i = 0; i < verticalLinesQty; i++)
        {
            List<Line> horizontalLines = new List<Line>();
            Vec3 endPosUp = new Vec3(transform.up * verticalAperture * (-verticalLinesQty / 2) + i * transform.up * verticalAperture);
            for (int j = 0; j < horizontalLinesQty; j++)
            {
                Vec3 startPos = new Vec3(transform.position);
                Vec3 endPosForward = new Vec3(transform.forward * lineDistance);
                Vec3 endPosRight = new Vec3(transform.right * horizontalAperture * (-horizontalLinesQty / 2) + j * transform.right * horizontalAperture);

                Vec3 endPos = new Vec3(endPosForward + endPosRight + endPosUp);
                endPos.Normalize();
                horizontalLines.Add(new Line(startPos, endPos));
            }
            bidimensionalLines.Add(horizontalLines);
        }

        foreach (List<Line> lines in bidimensionalLines)
        {
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
    }
    private void Update()
    {
        UpdateLines();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        foreach (List<Line> lines in bidimensionalLines)
        {
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
    }

    private void UpdateLines()
    {
        for (int i = 0; i < verticalLinesQty; i++)
        {
            Vec3 endPosUp = new Vec3(Camera.main.transform.up * verticalAperture * (-verticalLinesQty / 2) + i * Camera.main.transform.up * verticalAperture);
            for (int j = 0; j < horizontalLinesQty; j++)
            {
                Vec3 startPos = new Vec3(transform.position);
                Vec3 endPosForward = new Vec3(Camera.main.transform.forward * lineDistance);
                Vec3 endPosRight = new Vec3(Camera.main.transform.right * horizontalAperture * (-horizontalLinesQty / 2) + j * Camera.main.transform.right * horizontalAperture);

                Vec3 endPos = new Vec3(endPosForward + endPosRight + endPosUp);
                bidimensionalLines[i][j].SetLine(startPos, endPos);

                for (int k = 0; k < lineDistance / pointRate; k++)
                {
                    Vec3 point = bidimensionalLines[i][j].startPos + bidimensionalLines[i][j].endPos * pointRate * k;
                    bidimensionalLines[i][j].points[k] = point;
                }
            }
        }
    }
}