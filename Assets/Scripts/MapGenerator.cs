using UnityEngine;
using Unity;
using Unity.Entities;
public class MapGenerator : MonoBehaviour
{
    public WallBehaviour wallPrefab;
    public DoorBehaviour doorPrefab;

    // Constructor
    public MapGenerator(WallBehaviour wallPrefab, DoorBehaviour doorPrefab)
    {
        this.wallPrefab = wallPrefab;
        this.doorPrefab = doorPrefab;
    }


    public void GenerateMap()
    {
        int doorCount = 0;
        WallBehaviour wall;
        DoorBehaviour door;
        // Generates a quick map
        for (float x = -5.25f; x <= 5.25f; x += 0.5f)
        {
            for(float y = -2.75f; y <= 2.75f; y += 0.5f)
            {
                if ((x == 5.25f || x == -5.25f || y == -2.75f || y == 2.75f))
                {
                    if ((y >= -0.25f && y <= 0.25f) || (x >= -0.25 && x <= 0.25))
                    {
                        // It's possibly a door
                        door = Instantiate(doorPrefab);
                        
                        door.SetPosition(new Vector3(x, y, 0));
                        door.LevelTransition = doorCount;
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
}
