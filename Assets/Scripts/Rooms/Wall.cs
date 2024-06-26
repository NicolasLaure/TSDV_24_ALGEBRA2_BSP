using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Wall : MonoBehaviour
{
    const int DOOR_WIDTH = 2;
    const int DOOR_HEIGHT = 4;
    private Self_Plane plane;
    [SerializeField] private bool hasDoor;
    //  public Self_Plane Plane { get { return plane; } }
    public bool HasDoor { get { return hasDoor; } }
    public int DoorWidth { get { return DOOR_WIDTH; } }
    private void Awake()
    {
        plane = new Self_Plane(new Vec3(transform.forward), new Vec3(-transform.position));
        // WallDoor = new Door(new Vec3(transform.position), new Vec3(-transform.right * (DOOR_WIDTH / 2)), new Vec3(transform.right * (DOOR_WIDTH / 2)));
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Color lowOpacityGreen = Color.green - new Color(0, 0, 0, 0.7f);
        Gizmos.color = lowOpacityGreen;
        //Vec3 planePos = new Vec3(plane.Distance * plane.Normal) + new Vec3(0, transform.position.y, 0);
        //Vec3 planePos = new Vec3(transform.position);
        Gizmos.DrawCube(transform.position, new Vec3(0.5f, 0.5f, 0.5f));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + plane.Normal);

        if (hasDoor)
        {
            Gizmos.color = lowOpacityGreen;
            float lowerPos = transform.position.y - DOOR_HEIGHT / 2;
            float upperPos = transform.position.y + DOOR_HEIGHT / 2;
            Vec3 leftPos = new Vec3(transform.right * DOOR_WIDTH / 2);
            // Vec3 rightPos = transform.position.x + DOOR_WIDTH / 2;

            Gizmos.DrawLine(new Vec3(transform.position.x, upperPos, transform.position.z) - leftPos, new Vec3(transform.position.x, upperPos, transform.position.z) + leftPos);
            Gizmos.DrawLine(new Vec3(transform.position.x, lowerPos, transform.position.z) - leftPos, new Vec3(transform.position.x, lowerPos, transform.position.z) + leftPos);
            Gizmos.DrawLine(new Vec3(transform.position.x, lowerPos, transform.position.z) - leftPos, new Vec3(transform.position.x, upperPos, transform.position.z) - leftPos);
            Gizmos.DrawLine(new Vec3(transform.position.x, lowerPos, transform.position.z) + leftPos, new Vec3(transform.position.x, upperPos, transform.position.z) + leftPos);
        }
    }

    public bool IsPointOnPositiveSide(Vec3 point)
    {
        return plane.GetSide(point);
    }
}

public class Door
{
    private Vec3 minWidth;
    private Vec3 maxWidth;
    public Vec3 MinWidth { get; }
    public Vec3 MaxWidth { get; }

    public Door(Vec3 position, Vec3 minWidth, Vec3 maxWidth)
    {
        this.minWidth = minWidth;
        this.maxWidth = maxWidth;
    }
}