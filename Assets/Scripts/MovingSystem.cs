using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Components;
using EntityComponents;

public class MovingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation,
            ref MovementComponent moveComponent,
            ref ProjectileStatsComponent projectileStatsComponent) =>
        {
            if (moveComponent.currMovementDirection == Dir.East) {
                translation.Value.x += projectileStatsComponent.SpeedModifier * Time.DeltaTime;
            }
            if (moveComponent.currMovementDirection == Dir.West) {
                translation.Value.x -= projectileStatsComponent.SpeedModifier * Time.DeltaTime;
            }
            if (moveComponent.currMovementDirection == Dir.North)
            {
                translation.Value.y += projectileStatsComponent.SpeedModifier * Time.DeltaTime;
            }
            if (moveComponent.currMovementDirection == Dir.South)
            {
                translation.Value.y -= projectileStatsComponent.SpeedModifier * Time.DeltaTime;
            }
        });
    }
}
