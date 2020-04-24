using UnityEngine;

public class MapLogic
{
    public FloorInfo currentFloorInfo;
    public Room currentRoom;
    private int numberOfRooms;
    private int MAX_ROOMS_PER_FLOOR;
    // Constructor
    public void Setup(int maxRoomsPerFloor)
    {
        MAX_ROOMS_PER_FLOOR = maxRoomsPerFloor;
        // Set up floor logic
        currentFloorInfo = new FloorInfo(new Room(0));
        numberOfRooms = 1;
        CreateFloorLogic();
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

    public void CreateFloorLogic()
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

            direction = -1;
        }
        else
        {
            direction = (direction + 1) % 4;
        }
    }
}
