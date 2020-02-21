using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using EntityComponents;
using Unity.Mathematics;

public class HealthSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity e, ref EnemyStatsComponent enemyStatsComponent) =>
        {
            if (enemyStatsComponent.health <= 0)
            {
                PostUpdateCommands.DestroyEntity(e);
            }
        });
    }
}
