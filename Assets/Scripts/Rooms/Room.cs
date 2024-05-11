using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Room> adjacentRooms = new List<Room>();
    [SerializeField] private List<Wall> walls = new List<Wall>();
    public List<Wall> Walls { get { return walls; } }


    public bool shouldBeDrawn = false;
    public bool isChecked = false;
    private void Start()
    {
        foreach (Wall wall in transform.GetComponentsInChildren<Wall>())
        {
            walls.Add(wall);
        }
    }

    public void Show()
    {
        foreach (MeshRenderer mesh in transform.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (MeshRenderer mesh in transform.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
    }
}
