﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using Unity.Mathematics;
using System;
using EntityComponents;

public class CollisionSystem : ComponentSystem
{
    EntityManager entityManager;

    protected override void OnCreateManager()
    {
        base.OnCreateManager();

        entityManager = World.Active.EntityManager;
    }
    protected override void OnUpdate()
    {
        checkCollision<ProjectileStatsComponent, WallComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            PostUpdateCommands.DestroyEntity(entity1);
            Debug.Log("PROJECTILE COLLISION");
            return 0;
        });
        checkCollision<ProjectileStatsComponent, EnemyComponent> (Shape.Square, (Entity entity1, Entity entity2) =>
        {
            ProjectileStatsComponent psc = entityManager.GetComponentData<ProjectileStatsComponent>(entity1);
            if (psc.IsFromPlayer)
            {
                PostUpdateCommands.DestroyEntity(entity1);


                // Update stats
                StatsComponent esc = entityManager.GetComponentData<StatsComponent>(entity2);
                entityManager.SetComponentData(entity2, new StatsComponent
                {
                    attack = esc.attack,
                    attackSpeed = esc.attackSpeed,
                    moveSpeed = esc.moveSpeed,
                    health = esc.health - 10
                });

                Debug.Log("PROJECTILE AND ENEMY COLLISION");
            }
            return 0;
        });

        checkCollision<PlayerComponent, EnemyComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            // Update stats
            StatsComponent esc = entityManager.GetComponentData<StatsComponent>(entity1);
            entityManager.SetComponentData(entity1, new StatsComponent
            {
                attack = esc.attack,
                attackSpeed = esc.attackSpeed,
                moveSpeed = esc.moveSpeed,
                health = esc.health - 10
            });

            Debug.Log("PLAYER AND ENEMY COLLISION");
            return 0;
        });
    }

    private enum Shape {
        Circle,
        Square
    }

    // Func<Component1, Component2, bool> func
    private void checkCollision<Component1, Component2>(Shape shape, Func<Entity, Entity, int> action)
            where Component1 : struct, IComponentData
            where Component2 : struct, IComponentData
    {
        Entities.ForEach((Entity entity1,
            ref Component1 component1,
            ref Translation translation1,
            ref ColliderComponent colliderComponent1) =>
        {
            float2 pos1 = new float2(translation1.Value.x, translation1.Value.y);
            float s1 = colliderComponent1.Size;

            Entities.ForEach((Entity entity2,
                ref Component2 component2,
                ref Translation translation2,
                ref ColliderComponent colliderComponent2) =>
            {
                float2 pos2 = new float2(translation2.Value.x, translation2.Value.y);
                float s2 = colliderComponent2.Size;

                if (AreOverlapping(pos1, s1, pos2, s2, shape))
                {
                    action(entity1, entity2);
                }
            });
        });
    }

    // Checks if the square at position posA and size sizeA overlaps 
    // with the square at position posB and size sizeB
    private bool AreOverlapping(float2 posA, float sizeA, float2 posB, float sizeB, Shape shape)
    {
        bool overlapping = false;
        switch (shape)
        {
            case Shape.Square:
                float d = (sizeA / 2) + (sizeB / 2);
                overlapping |= (math.abs(posA.x - posB.x) < d && math.abs(posA.y - posB.y) < d);
                break;
            case Shape.Circle: // TODO : Do we want circle collisions?
                break;
        }

        return overlapping;
    }
}
