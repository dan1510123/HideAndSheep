using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MovingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MovingComponent moveComponent) =>
        {
            moveComponent.currMovementDirection += 1f + Time.DeltaTime;
        });
    }
}
