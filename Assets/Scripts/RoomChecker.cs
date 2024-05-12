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
        //CheckPoints();
        CheckLines();
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
                currentRoom = room;
                room.isChecked = true;
                room.shouldBeDrawn = true;
                continue;
            }
            room.isChecked = false;
            room.shouldBeDrawn = false;
        }
    }

    private void CheckLines()
    {
        foreach (Line line in lines.LinesList)
        {
            CheckAdjacentRooms(currentRoom, line.points, 0);
        }
    }
    //private void CheckPoints()
    //{
    //    foreach (Line line in lines.LinesList)
    //    {
    //        foreach (Room room in currentRoom.AdjacentRooms)
    //        {
    //            if (room.isChecked)
    //                continue;

    //            foreach (Vec3 point in line.points)
    //            {
    //                if (room.IsPointInsideRoom(new Vec3(point)))
    //                {
    //                    room.isChecked = true;
    //                    room.shouldBeDrawn = true;
    //                    continue;
    //                }
    //            }
    //        }
    //    }
    //}
    public Room RoomAPointIsIn(Vec3 point, List<Room> roomsList)
    {
        foreach (Room room in roomsList)
        {
            if (room.IsPointInsideRoom(new Vec3(point)))
                return room;
        }
        return null;
    }
    private void CheckAdjacentRooms(Room startingRoom, List<Vec3> points, int startingIndex)
    {
        for (int i = 0; i < points.Count; i++)
        {
            Room adjRoom = RoomAPointIsIn(points[i], startingRoom.AdjacentRooms);
            if (adjRoom != null && !adjRoom.isChecked)
            {
                adjRoom.isChecked = true;
                adjRoom.shouldBeDrawn = true;
                CheckAdjacentRooms(adjRoom, points, i);
            }
        }
    }
}
