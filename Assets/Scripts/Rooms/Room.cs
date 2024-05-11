using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Room> adjacentRooms = new List<Room>();
    [SerializeField] private List<Wall> walls = new List<Wall>();

    private void Start()
    {
        foreach (Wall wall in transform.GetComponentsInChildren<Wall>())
        {
            walls.Add(wall);
        }
    }
}
