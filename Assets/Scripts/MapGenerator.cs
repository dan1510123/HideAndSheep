﻿using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public EnvironmentUnit environmentPrefab;

    // Constructor
    public MapGenerator(EnvironmentUnit envPrefab)
    {
        this.environmentPrefab = envPrefab;
    }

    public void GenerateMap()
    {
        EnvironmentUnit env;
        // Generates a quick map
        for (int x = -5; x <= 5; x++)
        {
            for(int y = -4; y <=4; y++)
            {
                if (x == 5 || x == -5 || y == 4 || y == -4)
                {
                    env = Instantiate(environmentPrefab);
                    env.position = new Vector3(x, y, 0);
                }
            }
        }
    }
}