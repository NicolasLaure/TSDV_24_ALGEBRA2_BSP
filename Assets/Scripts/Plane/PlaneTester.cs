using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class PlaneTester : MonoBehaviour
{
    private Plane plane;
    private Self_Plane selfPlane;

    [SerializeField] private Vec3 normal = new Vec3(0, 1, 0);
    [SerializeField] private Vec3 position = new Vec3(0, 1, 0);
    [Space]
    [SerializeField] private float distance = 0;

    [Header("Set with three points")]
    [SerializeField] private Vec3 a;
    [SerializeField] private Vec3 b;
    [SerializeField] private Vec3 c;
    [Space]
    [SerializeField] private Vec3 translation;
    [SerializeField] private Vec3 point0;
    [SerializeField] private Vec3 point1;


    [Header("Visuals")]
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private Material planeMat;
    [SerializeField] private Material selfPlaneMat;
    [SerializeField] private Transform pointVisual0;
    [SerializeField] private Transform pointVisual1;
    [SerializeField] private Transform closestPlanePointVisual;
    [SerializeField] private Transform closestSelfPlanePointVisual;
    private GameObject planeObject;
    private GameObject selfPlaneObject;
    private void Start()
    {
        //plane = new Plane(a, b, c);
        //selfPlane = new Self_Plane(a, b, c);
        plane = new Plane(normal, position);
        selfPlane = new Self_Plane(normal, position);

        Debug.Log($"Unity Plane distance is:{plane.distance}");
        Debug.Log($"Self Plane distance is:{selfPlane.Distance}");

        planeObject = Instantiate(planePrefab);
        selfPlaneObject = Instantiate(planePrefab);
        planeObject.GetComponent<MeshRenderer>().material = planeMat;
        selfPlaneObject.GetComponent<MeshRenderer>().material = selfPlaneMat;
    }
    private void Update()
    {
        plane.SetNormalAndPosition(normal,position);
        //plane = Plane.Translate(plane, translation);
        planeObject.transform.up = plane.normal;
        planeObject.transform.position = plane.distance * plane.normal;

        //selfPlane = Self_Plane.Translate(selfPlane, translation);
        selfPlane.SetNormalAndPosition(normal,position);
        selfPlaneObject.transform.up = selfPlane.Normal;
        selfPlaneObject.transform.position = selfPlane.Distance * selfPlane.Normal;

        closestPlanePointVisual.position = plane.ClosestPointOnPlane(point0);
        closestSelfPlanePointVisual.position = selfPlane.ClosestPointOnPlane(point0);


        pointVisual0.position = point0;
        pointVisual1.position = point1;
        //Debug.Log($"UnityPlane point is on positive side = {plane.GetSide(point0)}");
        //Debug.Log($"SelfPlane point is on positive side = {selfPlane.GetSide(point0)}");
        Debug.Log($"Unity are two points on same side {plane.SameSide(point0,point1)}");
        Debug.Log($"SelfPlane are two points on same side {selfPlane.SameSide(point0,point1)}");

        //Debug.Log($"Unity distance to point: {plane.GetDistanceToPoint(point)}");
        //Debug.Log($"Self plane distance to point: {selfPlane.GetDistanceToPoint(point)}");
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(plane.normal * (plane.distance + 0.1f), (plane.normal * (plane.distance + 0.1f)) + plane.normal);
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawLine(selfPlane.Normal * selfPlane.Distance, (selfPlane.Normal * selfPlane.Distance) + selfPlane.Normal);
        }
    }
}
