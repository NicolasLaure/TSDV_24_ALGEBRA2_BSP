using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
public class Room : MonoBehaviour
{
    [SerializeField] private List<Room> adjacentRooms = new List<Room>();
    [SerializeField] private List<Self_Plane> wallPlanes = new List<Self_Plane>();
}
