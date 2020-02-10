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
        Entities.ForEach((ref Translation translation, ref MovementComponent moveComponent, ref ProjectileComponent p) =>
        {
            if (moveComponent.currMovementDirection == Dir.East) {
                translation.Value.x += 0.1f;
            }
            if (moveComponent.currMovementDirection == Dir.West) {
                translation.Value.x -= 0.1f;
            }
            if (moveComponent.currMovementDirection == Dir.North)
            {
                translation.Value.y += 0.1f;
            }
            if (moveComponent.currMovementDirection == Dir.South)
            {
                translation.Value.y -= 0.1f;
            }
        });
    }
}
