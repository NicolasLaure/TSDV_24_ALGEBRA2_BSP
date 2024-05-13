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
        if (currentRoom != null)
        {
            CheckAdjacentRooms(currentRoom, 0);
        }

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

    public Room RoomAPointIsIn(Vec3 point, List<Room> roomsList)
    {
        foreach (Room room in roomsList)
        {
            if (room.IsPointInsideRoom(new Vec3(point)))
                return room;
        }
        return null;
    }
    private void CheckAdjacentRooms(Room startingRoom, int startingIndex)
    {
        foreach (Line line in lines.LinesList)
        {
            for (int i = startingIndex; i < line.points.Count; i++)
            {
                Room adjRoom = RoomAPointIsIn(line.points[i], startingRoom.AdjacentRooms);
                if (adjRoom != null && !adjRoom.isChecked)
                {
                    Wall wallBetweenRooms = GetWallBetweenRooms(line.points[i], startingRoom);
                    if (wallBetweenRooms && wallBetweenRooms.HasDoor && DoesLinePassThroughDoor(line, wallBetweenRooms))
                    {
                        adjRoom.isChecked = true;
                        adjRoom.shouldBeDrawn = true;
                        CheckAdjacentRooms(adjRoom, i);
                    }
                }
            }
        }
    }

    private Wall GetWallBetweenRooms(Vec3 point, Room currentRoom)
    {
        foreach (Wall wall in currentRoom.Walls)
        {
            if (!wall.IsPointOnPositiveSide(point))
                return wall;
        }
        return null;
    }

    private bool DoesLinePassThroughDoor(Line line, Wall dooredWall)
    {
        Vec3 prevPoint = Vec3.Zero;
        foreach (Vec3 point in line.points)
        {
            if (prevPoint != Vec3.Zero && dooredWall.IsPointOnPositiveSide(prevPoint) && !dooredWall.IsPointOnPositiveSide(point))
            {
                Vec3 doorLeftPos = new Vec3(dooredWall.transform.right * dooredWall.DoorWidth / 2);

                bool isPrevPointAligned = (prevPoint.x > (dooredWall.transform.position - doorLeftPos).x && prevPoint.x < (dooredWall.transform.position + doorLeftPos).x) ||
                                          (prevPoint.z > (dooredWall.transform.position - doorLeftPos).z && prevPoint.z < (dooredWall.transform.position + doorLeftPos).z);

                bool isPointAligned = (point.x > (dooredWall.transform.position - doorLeftPos).x && point.x < (dooredWall.transform.position + doorLeftPos).x) ||
                                      (point.z > (dooredWall.transform.position - doorLeftPos).z && point.z < (dooredWall.transform.position + doorLeftPos).z);

                if (isPrevPointAligned && isPointAligned)
                    return true;
            }
            prevPoint = point;
        }
        return false;
    }
}