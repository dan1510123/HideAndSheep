using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Components;
using EnvironmentComponents;

public class MovingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {

        Entities.ForEach((Entity e,
            ref Translation translation,
            ref ProjectileStatsComponent projectileStats) =>
        {
            
            translation.Value.x += projectileStats.Direction.x * 0.1f;
            translation.Value.y += projectileStats.Direction.y * 0.1f;
        });
    }
}
