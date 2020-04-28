using UnityEngine;
using System.Collections;

public class Room
{
    public Room[] rooms = new Room[4];
    public Vector3[] enemies;
    public Vector3[] obstacles;
    public int depth;
    public bool roomFound = false;
    public bool finalRoom = false;

    public Room(int depth)
    {
        this.depth = depth;
    }

    public int GetNumberOfConnectingRooms()
    {
        int numRooms = 0;
        foreach(Room r in rooms)
        {
            if(r != null)
            {
                numRooms++;
            }
        }
        return numRooms;
    }

    public override string ToString() {
        string s = "Depth " + depth + ", rooms: ";
        for(int i = 0; i < 4; i++)
        {
            if(rooms[i] != null)
            {
                switch(i)
                {
                    case 0:
                        s += "north ";
                        break;
                    case 1:
                        s += "east ";
                        break;
                    case 2:
                        s += "south ";
                        break;
                    case 3:
                        s += "west ";
                        break;
                }
            }
        }

        return s;
    }
}
