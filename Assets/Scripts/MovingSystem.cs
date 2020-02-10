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
            if(moveComponent.currMovementDirection == Dir.East) {
                translation.Value.x += 1f;
            }
        });
    }
}
