using UnityEngine;
using Unity;
using Unity.Entities;
using System;
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private WallBehaviour wallPrefab;
    [SerializeField] private DoorBehaviour doorPrefab;

    //Map generator shift amount.
    private float horizontalShift;
    private float verticalShift;

    // Constructor
    public MapGenerator(WallBehaviour wallPrefab, DoorBehaviour doorPrefab)
    {
        this.wallPrefab = wallPrefab;
        this.doorPrefab = doorPrefab;
    }


    //Generates walls and doors for a room
    public void GenerateRoom(float horizontalShift, float verticalShift)
    {
        int doorCount = 0;
        int transition = 0;
        WallBehaviour wall;
        DoorBehaviour door;
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
                    if ((minDoorYBound <= y && y <= maxDoorYBound) || (minDoorXBound <= x && x <= maxDoorXBound))
                    {
                        // It's possibly a door
                        door = Instantiate(doorPrefab);
                        
                        door.SetPosition(new Vector3(x, y, 0));
                        if(isNewDoor(doorCount))
                        {
                            Debug.Log("Level transition is " + transition);
                            transition++;
                        }
                        door.LevelTransition = transition;
                        doorCount++;

                    }
                    else
                    {
                        // It's a wall
                        wall = Instantiate(wallPrefab);
                        wall.SetPosition(new Vector3(x, y, 0));
                    }
                }
            }
        }
    }

    bool isNewDoor(int doorCount)
    {
        Debug.Log("Door count is " + doorCount);
        return doorCount % 2 == 0;
    }
}
