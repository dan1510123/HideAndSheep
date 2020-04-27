using UnityEngine;

public class MapLogic
{
    public FloorInfo currentFloorInfo;
    public Room currentRoom;
    private int numberOfRooms;
    private int MAX_ROOMS_PER_FLOOR;
    private int MAX_ENEMIES_PER_FLOOR;
    private int MAX_OBSTACLES_PER_FLOOR;

    // Constructor
    public void Setup(int maxRoomsPerFloor)
    {
        MAX_ROOMS_PER_FLOOR = maxRoomsPerFloor;
        MAX_ENEMIES_PER_FLOOR = 3;
        MAX_OBSTACLES_PER_FLOOR = 8;
        // Set up floor logic
        currentFloorInfo = new FloorInfo(new Room(0));
        numberOfRooms = 1;
        CreateFloorLogic(MAX_ROOMS_PER_FLOOR);
        currentRoom = currentFloorInfo.getStartRoom();
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
        for (int i = 0; i < 4; i++)
        {
            Room r = newRoom.rooms[i];
            if (r != null && !r.Equals(oldRoom))
            {
                PrintRoomRecurse(ref newRoom, ref r);
            }
        }
    }

    public void CreateFloorLogic(int MAX_ROOMS_PER_FLOOR)
    {
        Room currentRoomForLogic = currentFloorInfo.getStartRoom();
        int nextMainDirection = 0;

        while (numberOfRooms < MAX_ROOMS_PER_FLOOR)
        {
            int addedRooms = Random.Range(1, 2);

            for (int i = 0; i < addedRooms && numberOfRooms < MAX_ROOMS_PER_FLOOR; i++)
            {
                int direction = Random.Range(0, 3);
                if (i == 0)
                {
                    nextMainDirection = direction;
                }

                while (direction != -1)
                {
                    CreateRoomOrShiftDirection(ref currentRoomForLogic, ref direction);
                }

                numberOfRooms++;
            }

            currentRoomForLogic = currentRoomForLogic.rooms[nextMainDirection];
        }
    }

    private void CreateRoomOrShiftDirection(ref Room currentRoom, ref int direction)
    {
        if (currentRoom.rooms[direction] == null)
        {
            currentRoom.rooms[direction] = new Room(currentRoom.depth + 1);
            currentRoom.rooms[direction].rooms[(direction + 2) % 4] = currentRoom;
            currentRoom.rooms[direction].enemies = getRandomEnemyRatioLocations();
            currentRoom.rooms[direction].obstacles = getRandomObstacleRatioLocations();

            direction = -1;
        }
        else
        {
            direction = (direction + 1) % 4;
        }
    }

    private Vector3[] getRandomEnemyRatioLocations()
    {
        return getRandomEntityRatioLocations(MAX_ENEMIES_PER_FLOOR);

    }

    private Vector3[] getRandomObstacleRatioLocations()
    {
        return getRandomEntityRatioLocations(MAX_OBSTACLES_PER_FLOOR);
    }

    // Returns array of Vector3 between .2 and 1.0 ratio
    private Vector3[] getRandomEntityRatioLocations(int MAX_ENTITIES_PER_FLOOR)
    {
        int numEntities = Random.Range(1, MAX_ENTITIES_PER_FLOOR);
        Vector3[] entities = new Vector3[numEntities];

        for (int i = 0; i < numEntities; i++)
        {
            float x = Random.Range(2, 10) / 10f;
            float y = Random.Range(2, 10) / 10f;
            int xNeg = Random.Range(0, 2);
            int yNeg = Random.Range(0, 2);
            x *= xNeg == 0 ? 1 : -1;
            y *= yNeg == 0 ? 1 : -1;

            entities[i] = new Vector3(x, y, 0);
        }

        return entities;
    }
}
