using UnityEngine;
using System.Collections;
using Unity;
using Unity.Entities;
public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private WallBehaviour wallPrefab;
    [SerializeField] private DoorBehaviour doorPrefab;
    private FloorInfo currentFloorInfo;
    private Room currentRoom;
    private int numberOfRooms;
    private int MAX_ROOMS_PER_FLOOR = 3;

    //Map generator shift amount.
    private float horizontalShift;
    private float verticalShift;

    // Constructor
    public void Setup()
    {
        currentFloorInfo = new FloorInfo(new Room(0));
        currentRoom = currentFloorInfo.getStartRoom();
        numberOfRooms = 1;
        GenerateRoom(ref currentRoom, 0, 0);
        CreateFloorLogic();
        PrintFloor();
    }

    private void PrintFloor()
    {
        Room start = currentFloorInfo.getStartRoom();
        PrintRoomRecurse(ref start, ref start);
    }

    private void PrintRoomRecurse(ref Room oldRoom, ref Room newRoom)
    {
        Debug.Log(newRoom);
        for(int i = 0; i < 4; i++)
        {
            Room r = newRoom.rooms[i];
            if (r != null && !r.Equals(oldRoom))
            {
                PrintRoomRecurse(ref newRoom, ref r);
            }
        }
    }

    public void CreateFloorLogic()
    {
        Room currentRoom = currentFloorInfo.getStartRoom();
        int nextMainDirection = 0;

        while (numberOfRooms < MAX_ROOMS_PER_FLOOR)
        {
            int addedRooms = Random.Range(1, 2);

            for(int i = 0; i < addedRooms && numberOfRooms < MAX_ROOMS_PER_FLOOR; i++)
            {
                int direction = Random.Range(0, 3);
                if(i == 0)
                {
                    nextMainDirection = direction;
                }

                while (direction != -1)
                {
                    CreateRoomOrShiftDirection(ref currentRoom, ref direction);
                }

                numberOfRooms++;
            }

            currentRoom = currentRoom.rooms[nextMainDirection];
        }
    }

    private void CreateRoomOrShiftDirection(ref Room currentRoom, ref int direction)
    {
        if(currentRoom.rooms[direction] == null)
        {
            currentRoom.rooms[direction] = new Room(currentRoom.depth + 1);
            currentRoom.rooms[direction].rooms[(direction + 2) % 4] = currentRoom;

            direction = -1;
        }
        else
        {
            direction = (direction + 1) % 4;
        }
    }

    //Generates walls and doors for a room
    public void GenerateRoom(ref Room currentRoom, float horizontalShift, float verticalShift)
    {
        int doorCount = 0;
        int transition = 0;
        WallBehaviour wall;

        int doorUnitCount = 0;

        //Wall bounds
        float minWallXBound = -5.25f + horizontalShift;
        float maxWallXBound = 5.25f + horizontalShift;

        float minWallYBound = -2.75f + verticalShift;
        float maxWallYBOund = 2.75f + verticalShift;

        //Door bounds
        float minDoorXBound = -.25f + horizontalShift;
        float maxDoorXBound = .25f + horizontalShift;

        float minDoorYBound = -.25f + verticalShift;
        float maxDoorYBound = .25f + verticalShift;

        // Generates a quick map
        for (float x = minWallXBound; x <= maxWallXBound; x += 0.5f)
        {
            for(float y = minWallYBound; y <= maxWallYBOund; y += 0.5f)
            {
                if ((x == minWallXBound || x == maxWallXBound || y == minWallYBound || y == maxWallYBOund))
                {
                    bool isWall = true;
                    if ((minDoorYBound <= y && y <= maxDoorYBound) || (minDoorXBound <= x && x <= maxDoorXBound))
                    {
                        isWall = false;
                        doorUnitCount++;
                    }
                    if(isWall)
                    {
                        // It's a wall
                        wall = Instantiate(wallPrefab);
                        wall.SetPosition(new Vector3(x, y, 0));
                    }
                }
            }
        }
    }

    //private bool DoorNeeded(float x, float y)
    //{

    //}

    //private void AddDoor(float x, float y)
    //{
    //    DoorBehaviour door;

    //    // It's possibly a door
    //    door = Instantiate(doorPrefab, new Vector3(100, 100, 0), doorPrefab.transform.rotation);

    //    door.SetPosition(new Vector3(x, y, 0));
    //    if (isNewDoor(doorCount))
    //    {
    //        Debug.Log("Level transition is " + transition);
    //        transition++;
    //    }
    //    door.LevelTransition = transition;
    //    doorCount++;
    //}

    //bool isNewDoor(int doorCount)
    //{
    //    Debug.Log("Door count is " + doorCount);
    //    return doorCount % 2 == 0;
    //}
}
