using System.Collections.Generic;
using System.Linq;
using CustomMath;
using UnityEngine;

public class RoomCheckerV2 : MonoBehaviour
{
    [SerializeField] Lines lines;
    [SerializeField] private int steps;
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
            CheckLines(currentRoom, 0);
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

    private void CheckLines(Room startingRoom, int startingIndex)
    {
        foreach (List<Line> lines in lines.LinesList)
        {
            foreach (Line line in lines)
            {
                line.points.Clear();
                CheckLine(currentRoom, line, line.startPos, line.endPos, steps);
            }
        }
    }

    private void CheckLine(Room referenceRoom, Line line, Vec3 startPos, Vec3 endPos, int stepsCount)
    {
        if (stepsCount < 1)
            return;

        Vec3 middlePos = Vec3.Lerp(startPos, endPos, 0.5f);
        line.points.Add(middlePos);
        if (referenceRoom.IsPointInsideRoom(middlePos))
        {
            CheckLine(referenceRoom, line, middlePos, line.endPos, stepsCount - 1);
        }
        else
        {
            Room adjRoom = RoomAPointIsIn(middlePos, referenceRoom.AdjacentRooms);
            if (adjRoom == null)
                return;

            if (!adjRoom.isChecked)
            {
                Wall wallBetweenRooms = GetWallBetweenRooms(middlePos, referenceRoom);
                if (wallBetweenRooms && wallBetweenRooms.HasDoor && DoesLinePassThroughDoor(new Line(startPos, middlePos), wallBetweenRooms))
                {
                    adjRoom.isChecked = true;
                    adjRoom.shouldBeDrawn = true;
                }
            }

            if (adjRoom.isChecked)
                CheckLine(adjRoom, line, middlePos, line.endPos, stepsCount - 1);
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
        if (line.points.Count == 0)
        {
            line.points.Add(line.startPos);
            line.points.Add(line.endPos);
        }

        foreach (Vec3 point in line.points)
        {
            if (prevPoint != Vec3.Zero && dooredWall.IsPointOnPositiveSide(prevPoint) && !dooredWall.IsPointOnPositiveSide(point))
            {
                Vec3 doorLeftPos = new Vec3(dooredWall.transform.right * dooredWall.DoorWidth / 2);
                Vec3 middlePoint = (point + prevPoint) / 2;
                bool isPointAligned = (middlePoint.x > (dooredWall.transform.position - doorLeftPos).x && middlePoint.x < (dooredWall.transform.position + doorLeftPos).x) ||
                                      (middlePoint.x < (dooredWall.transform.position - doorLeftPos).x && middlePoint.x > (dooredWall.transform.position + doorLeftPos).x) ||
                                      (middlePoint.z > (dooredWall.transform.position - doorLeftPos).z && middlePoint.z < (dooredWall.transform.position + doorLeftPos).z) ||
                                      (middlePoint.z < (dooredWall.transform.position - doorLeftPos).z && middlePoint.z > (dooredWall.transform.position + doorLeftPos).z);

                return isPointAligned;
            }

            prevPoint = point;
        }

        return false;
    }

    private bool DoesLinePassesThroughAllDoors(Line line)
    {
        List<Wall> intermediateWalls = new List<Wall>();
        Room prevRoom = currentRoom;
        foreach (Vec3 point in line.points)
        {
            Room roomPointIsIn = RoomAPointIsIn(point, rooms.ToList());
            if (roomPointIsIn && roomPointIsIn != prevRoom)
            {
                Wall wallBetween = GetWallBetweenRooms(point, prevRoom);
                if (wallBetween && wallBetween.HasDoor)
                {
                    intermediateWalls.Add(wallBetween);
                    prevRoom = roomPointIsIn;
                }
                else
                    return false;
            }
        }

        foreach (Wall wall in intermediateWalls)
        {
            if (!wall.HasDoor || !DoesLinePassThroughDoor(line, wall))
                return false;
        }

        return true;
    }
}