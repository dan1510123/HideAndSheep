using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using Unity.Mathematics;
using System;
using EntityComponents;
using ItemComponent;
using BackpackComponents;

public class RoomSystem : ComponentSystem
{
    EntityManager entityManager;
    Camera camera;

    protected override void OnCreateManager()
    {
        base.OnCreateManager();

        entityManager = World.Active.EntityManager;
        camera = GameObject.FindObjectOfType<Camera>();
    }
    protected override void OnUpdate()
    {
        int enemiesLeft = 0;
        Entities.ForEach((Entity e,
            ref EnemyComponent enemyComponent) =>
        {
            enemiesLeft++;
        });

        if (enemiesLeft == 0)
        {
            Entities.ForEach((Entity e,
            ref DoorComponent doorComponent,
            ref WallComponent wallComponent) =>
            {
                doorComponent.locked = false;
            });
        }

        Entities.ForEach((Entity e,
            ref DoorComponent doorComponent,
            ref WallComponent wallComponent) =>
        {
            if(doorComponent.locked == false)
            {
                entityManager.RemoveComponent<WallComponent>(e);
            }
        });

    }
}
