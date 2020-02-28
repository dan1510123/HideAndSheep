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
                if (CheckCollision(new float2(translation.Value.x, translation.Value.y), s1))
                {
                    projectileStatsComponent.Alive = false;
                }
                else if (moveComponent.currMovementDirection == Dir.East)
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
        });
    }

    bool CheckCollision(float2 pos1, float s1)
    {
        bool collided = false;
        Entities.ForEach((ref WallComponent wallComponent,
                ref Translation translation1,
                ref ColliderComponent colliderComponent1) =>
        {
            float2 pos2 = new float2(translation1.Value.x, translation1.Value.y);
            float s2 = colliderComponent1.Size;
            collided = collided || AreSquaresOverlapping(pos1, s1, pos2, s2);
        });

        return false;
    }

    // Checks if the square at position posA and size sizeA overlaps 
    // with the square at position posB and size sizeB
    bool AreSquaresOverlapping(float2 posA, float sizeA, float2 posB, float sizeB)
    {
        float d = (sizeA / 2) + (sizeB / 2);
        return (math.abs(posA.x - posB.x) < d && math.abs(posA.y - posB.y) < d);
    }
}
