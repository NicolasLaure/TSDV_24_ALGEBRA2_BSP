using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Wall : MonoBehaviour
{
    const int DOOR_WIDTH = 2;
    const int DOOR_HEIGHT = 4;
    [SerializeField] private bool hasDoor;
    private Self_Plane plane;

    private void Awake()
    {
        plane = new Self_Plane(new Vec3(transform.forward), new Vec3(transform.position));
    }
}
