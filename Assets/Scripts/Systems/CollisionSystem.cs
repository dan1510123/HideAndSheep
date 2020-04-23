﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using Unity.Mathematics;
using System;
using EntityComponents;
using ItemComponent;
using BackpackComponents;

public class CollisionSystem : ComponentSystem
{
    EntityManager entityManager;
    Camera camera;
    bool teleportingPlayerLock = false;
    
    protected override void OnCreateManager()
    {
        base.OnCreateManager();

        entityManager = World.Active.EntityManager;
        camera = GameObject.FindObjectOfType<Camera>();
    }
    protected override void OnUpdate()
    {
        checkCollision<ProjectileStatsComponent, WallComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            PostUpdateCommands.DestroyEntity(entity1);
            Debug.Log("PROJECTILE COLLISION");
            return 0;
        });

        checkCollision<ProjectileStatsComponent, EnemyComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            teleportingPlayerLock = false;
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

        checkCollision<ProjectileStatsComponent, PlayerComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            ProjectileStatsComponent psc = entityManager.GetComponentData<ProjectileStatsComponent>(entity1);
            if (!psc.IsFromPlayer)
            {
                PostUpdateCommands.DestroyEntity(entity1);

                // Update stats
                StatsComponent playerStats = entityManager.GetComponentData<StatsComponent>(entity2);
                entityManager.SetComponentData(entity2, new StatsComponent
                {
                    attack = playerStats.attack,
                    attackSpeed = playerStats.attackSpeed,
                    moveSpeed = playerStats.moveSpeed,
                    health = playerStats.health - 10
                });

                Debug.Log("PROJECTILE AND PLAYER COLLISION");
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


        checkCollision<PlayerComponent, ItemID>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            PostUpdateCommands.DestroyEntity(entity2);
            // Update stats
            StatsComponent esc = entityManager.GetComponentData<StatsComponent>(entity1);
            ItemStats itemStats = entityManager.GetComponentData<ItemStats>(entity2);
            entityManager.SetComponentData(entity1, new StatsComponent
            {
                attack = esc.attack + itemStats.attack,
                attackSpeed = esc.attackSpeed + itemStats.attackSpeed,
                moveSpeed = esc.moveSpeed + itemStats.moveSpeed,
                health = esc.health + itemStats.health
            });

            // Add itemID to backpack
            DynamicBuffer<IntBufferElement> backpack = entityManager.GetBuffer<IntBufferElement>(entity1);
            int itemID = entityManager.GetComponentData<ItemID>(entity2).id;
            backpack.Add(new IntBufferElement { value = itemID });

            Debug.Log("PLAYER AND ITEM COLLISION");
            return 0;
        });

        //Check for player collision between the player character and the door.
        checkCollision<PlayerComponent, DoorComponent>(Shape.Square, (Entity entity1, Entity entity2) =>
        {
            Debug.Log("Door position is " + EntityManager.GetComponentData<Translation>(entity2).Value);
            Debug.Log("Player position is " + EntityManager.GetComponentData<Translation>(entity1).Value);
            if (EntityManager.GetComponentData<DoorComponent>(entity2).locked == false && !teleportingPlayerLock)
            {
                teleportingPlayerLock = true;
                int doorTransition = getDoorTransition(entity2);
                Debug.Log("Door transition: " + doorTransition);
                switch (doorTransition)
                {
                    case 0:
                        GlobalObjects.cameraPosition.y += 30;
                        shiftCamera(0, 30);
                        break;
                    case 1:
                        GlobalObjects.cameraPosition.x += 30;
                        shiftCamera(30, 0);
                        break;
                    case 2:
                        GlobalObjects.cameraPosition.y -= 30;
                        shiftCamera(0, -30);
                        break;
                    case 3:
                        GlobalObjects.cameraPosition.x -= 30;
                        shiftCamera(-30, 0);
                        break;
                    default:
                        break;
                }

                Debug.Log(GlobalObjects.mapLogic.currentRoom);
                GlobalObjects.mapLogic.currentRoom = GlobalObjects.mapLogic.currentRoom.rooms[doorTransition];
                Debug.Log(GlobalObjects.mapLogic.currentRoom);

                GlobalObjects.mapBehaviour.GenerateRoomWalls(
                    GlobalObjects.mapLogic.currentRoom,
                    GlobalObjects.cameraPosition.x,
                    GlobalObjects.cameraPosition.y);

                Debug.Log("OPENED DOOR");
            }
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

    int getDoorTransition(Entity door)
    {
        return EntityManager.GetComponentData<DoorComponent>(door).levelTransition;
    }

    void shiftCamera(float x, float y)
    {
        this.camera.transform.position = camera.transform.position + new Vector3(x, y);
    }
}
