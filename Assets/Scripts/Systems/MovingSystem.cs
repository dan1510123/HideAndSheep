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
            ref MovementComponent moveComponent,
            ref ProjectileStatsComponent projectileStatsComponent,
            ref ColliderComponent colliderComponent) =>
        {
            if (!projectileStatsComponent.Alive)
            {
                PostUpdateCommands.DestroyEntity(e);
            }
            else
            {
                float s1 = colliderComponent.Size;
                float dt = Time.DeltaTime;
                float projectileDisplacement = projectileStatsComponent.SpeedModifier * dt;
                if (projectileStatsComponent.IsFromPlayer)
                {
                    if (moveComponent.currMovementDirection == Dir.East)
                    {
                        translation.Value.x += projectileDisplacement;
                    }
                    else if (moveComponent.currMovementDirection == Dir.West)
                    {
                        translation.Value.x -= projectileDisplacement;
                    }
                    else if (moveComponent.currMovementDirection == Dir.North)
                    {
                        translation.Value.y += projectileDisplacement;
                    }
                    else if (moveComponent.currMovementDirection == Dir.South)
                    {
                        translation.Value.y -= projectileDisplacement;
                    }
                }
                else
                {
                    // move projectile in direction
                    translation.Value.x += projectileStatsComponent.Direction.x * 0.05f;
                    translation.Value.y += projectileStatsComponent.Direction.y * 0.05f;
                    Debug.Log("in moving system" + translation.Value);
                }

            }
        });
    }

}
