using UnityEngine;
using Unity.Entities;
using EntityComponents;

public class HealthSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity e,
            ref PlayerComponent playerComponent,
            ref StatsComponent statsComponent) =>
        {
            if (statsComponent.health <= 0)
            {
                Debug.Log("Player killed");
                Audio.PlayDeathSound();
                Entities.ForEach((Entity e1) =>
                {
                    PostUpdateCommands.DestroyEntity(e1);
                });

                Application.LoadLevel(3);
            }
        });
        Entities.ForEach((Entity e,
            ref StatsComponent statsComponent) =>
        {
            if (statsComponent.health <= 0)
            {
                Debug.Log("Enemy killed");
                Audio.PlayDeathSound();
                PostUpdateCommands.DestroyEntity(e);
            }
        });
    }
}
