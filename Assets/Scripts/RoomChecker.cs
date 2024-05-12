using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class RoomChecker : MonoBehaviour
{
    //[SerializeField] Lines lines;

    Room[] rooms;
    Room currentRoom;
    private void Start()
    {
        rooms = FindObjectsOfType<Room>();
        foreach (Room room in rooms)
        {
            room.Hide();
        }
        CheckCurrentRoom();
    }

    private void CheckCurrentRoom()
    {
        foreach(Room room in rooms)
        {
            if (room.IsPointInsideRoom(new Vec3(transform.position)))
            {
                currentRoom = room;
                currentRoom.Show();
            }
        }
    }
}
