using UnityEngine;


public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private WallBehaviour wallPrefab;
    [SerializeField] private DoorBehaviour doorPrefab;

    // Constructor
    public void Setup(ref Room initialRoom)
    {
        // Generate first room
        GenerateRoomWalls(initialRoom, 0, 0);
        initialRoom.roomFound = true;
    }

    public void GenerateRoom(Room room, float horizontalShift, float verticalShift)
    {
        GenerateRoomWalls(room, horizontalShift, verticalShift);
        SpawnEnemies(room, horizontalShift, verticalShift);
        SpawnObstacles(room, horizontalShift, verticalShift);
    }

    private void SpawnEnemies(Room room, float horizontalShift, float verticalShift)
    {
        foreach(Vector3 loc in room.enemies) {
            GameObject e = Instantiate(GlobalObjects.enemyPrefab, new Vector3(100, 100, 0), GlobalObjects.enemyPrefab.transform.rotation);
            e.GetComponent<EnemyBehaviour>().setSpawn(new Vector3(horizontalShift + 5f * loc.x, verticalShift + 2.5f * loc.y, 0));
        }
    }

    private void SpawnObstacles(Room room, float horizontalShift, float verticalShift)
    {
        foreach (Vector3 loc in room.obstacles)
        {
            WallBehaviour wall = Instantiate(wallPrefab);
            wall.SetPosition(new Vector3(horizontalShift + 5f * loc.x, verticalShift + 2.5f * loc.y, 0));
            //GameObject e = Instantiate(GlobalObjects.wallPrefab, new Vector3(100, 100, 0), GlobalObjects.wallPrefab.transform.rotation);
            //e.GetComponent<WallBehaviour>().SetPosition(new Vector3(horizontalShift + 5f * loc.x, verticalShift + 2.5f * loc.y, 0));
        }
    }

    //Generates walls and doors for a room
    private void GenerateRoomWalls(Room room, float horizontalShift, float verticalShift)
    {
        int doorIndex = 0;

        //Wall bounds
        float minWallXBound = -4.8f + horizontalShift;
        float maxWallXBound = 4.8f + horizontalShift;

        float minWallYBound = -3.75f + verticalShift;
        float maxWallYBound = 3.75f + verticalShift;

        //Door bounds
        float minDoorXBound = -.5f + horizontalShift;
        float maxDoorXBound = .5f + horizontalShift;

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
