using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public WallBehaviour wallPrefab;

    // Constructor
    public MapGenerator(WallBehaviour wallPrefab)
    {
        this.wallPrefab = wallPrefab;
    }

    public void GenerateMap()
    {
        WallBehaviour env;
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
                    }
                    else
                    {
                        // It's a wall
                        env = Instantiate(wallPrefab);
                        env.SetPosition(new Vector3(x, y, 0));
                    }
                }
            }
        }
    }
}
