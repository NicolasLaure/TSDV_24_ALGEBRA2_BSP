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
        StartCoroutine(CheckCurrentRoom());
    }

    private IEnumerator CheckCurrentRoom()
    {
        yield return null;
        foreach (Room room in rooms)
        {
            if (IsPointInsideRoom(lines.LinesList[0].points[0], room))
            {
                currentRoom = room;
                break;
            }
        }
        if (currentRoom != null)
            currentRoom.Show();
    }
    private bool IsPointInsideRoom(Vec3 point, Room roomToCheck)
    {
        for (int i = 0; i < roomToCheck.Walls.Count; i++)
        {
            if (!roomToCheck.Walls[i].Plane.GetSide(point))
                return false;
        }
        return true;
    }
}
