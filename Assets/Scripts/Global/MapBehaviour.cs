using UnityEngine;
using System.Collections;
using Unity;
using Unity.Entities;
public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private WallBehaviour wallPrefab;
    [SerializeField] private DoorBehaviour doorPrefab;

    // Constructor
    public void Setup(Room initialRoom)
    {
        // Generate first room
        GenerateRoomWalls(initialRoom, 0, 0);
    }

    //Generates walls and doors for a room
    public void GenerateRoomWalls(Room room, float horizontalShift, float verticalShift)
    {
        int doorIndex = 0;

        //Wall bounds
        float minWallXBound = -5.25f + horizontalShift;
        float maxWallXBound = 5.25f + horizontalShift;

        float minWallYBound = -2.75f + verticalShift;
        float maxWallYBound = 2.75f + verticalShift;

        //Door bounds
        float minDoorXBound = -.25f + horizontalShift;
        float maxDoorXBound = .25f + horizontalShift;

        float minDoorYBound = -.25f + verticalShift;
        float maxDoorYBound = .25f + verticalShift;

        /* Generates walls of the map */
        // North wall
        CreateWall(ref doorIndex, room, minWallXBound, maxWallXBound, minDoorXBound, maxDoorXBound, maxWallYBound, "x");
        // East wall
        CreateWall(ref doorIndex, room, minWallYBound, maxWallYBound, minDoorYBound, maxDoorYBound, maxWallXBound, "y");
        // South wall
        CreateWall(ref doorIndex, room, minWallXBound, maxWallXBound, minDoorXBound, maxDoorXBound, minWallYBound, "x");
        // West wall
        CreateWall(ref doorIndex, room, minWallYBound, maxWallYBound, minDoorYBound, maxDoorYBound, minWallXBound, "y");
    }

    private void CreateWall(
        ref int doorIndex,
        Room room,
        float mainLowerBound,
        float mainUpperBound,
        float doorLowerBound,
        float doorUpperBound,
        float altBound,
        string axis)
    {
        for (float x = mainLowerBound; x <= mainUpperBound; x += 0.5f)
        {
            bool isWall = true;
            if ((doorLowerBound <= x && x <= doorUpperBound))
            {
                if (DoorNeeded(doorIndex, room))
                {
                    AddDoorUnit(doorIndex, x, altBound, axis);
                    isWall = false;
                }
            }

            if (isWall)
            {
                // It's a wall
                AddWallUnit(x, altBound, axis);
            }
        }
        doorIndex++;
    }

    private void AddWallUnit(float x, float y, string axis)
    {
        WallBehaviour wall = Instantiate(wallPrefab);
        if (axis.Equals("x"))
        {
            wall.SetPosition(new Vector3(x, y, 0));
        }
        else
        {
            wall.SetPosition(new Vector3(y, x, 0));
        }
    }

    private bool DoorNeeded(int doorIndex, Room room)
    {
        return room.rooms[doorIndex] != null;
    }

    private void AddDoorUnit(int doorIndex, float x, float y, string axis)
    {
        DoorBehaviour door;

        // It's possibly a door
        door = Instantiate(doorPrefab, new Vector3(100, 100, 0), doorPrefab.transform.rotation);
        if (axis.Equals("x"))
        {
            door.SetPosition(new Vector3(x, y, 0));
        }
        else
        {
            door.SetPosition(new Vector3(y, x, 0));
        }
        door.LevelTransition = doorIndex;
    }
}
