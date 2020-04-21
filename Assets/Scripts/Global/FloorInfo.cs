using UnityEngine;
using System.Collections;

public class FloorInfo
{
    private Room startRoom;

    public FloorInfo(Room startRoom)
    {
        this.startRoom = startRoom;
    }

    public Room getStartRoom()
    {
        return startRoom;
    }
}
