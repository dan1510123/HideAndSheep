using UnityEngine;
using Unity.Entities;

public class HealthSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity e,
            ref StatsComponent statsComponent) =>
        {
            if (statsComponent.health <= 0)
            {
                Debug.Log("Enemy killed");
                PostUpdateCommands.DestroyEntity(e);
            }
        });
    }
}
