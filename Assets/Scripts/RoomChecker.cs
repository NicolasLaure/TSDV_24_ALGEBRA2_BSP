using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class RoomChecker : MonoBehaviour
{
    [SerializeField] Lines lines;

    Room[] rooms;
    Room currentRoom;
    private void Start()
    {
        rooms = FindObjectsOfType<Room>();
        foreach (Room room in rooms)
        {
            room.Hide();
        }
    }
    private void Update()
    {
        CheckCurrentRoom();
        foreach (Room room in rooms)
        {
            if (room.shouldBeDrawn)
                room.Show();
            else
                room.Hide();
        }
    }
    private void CheckCurrentRoom()
    {
        foreach (Room room in rooms)
        {
            if (room.IsPointInsideRoom(new Vec3(transform.position)))
            {
                room.isChecked = true;
                room.shouldBeDrawn = true;
                continue;
            }
            room.isChecked = false;
            room.shouldBeDrawn = false;
        }
    }

    private void CheckPoints()
    {
        foreach (Line line in lines.LinesList)
        {

        }
    }
}
