using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Components;
using EnvironmentComponents;
using System;
public class DoorBehaviour : EnvironmentBehaviour<DoorComponent>
{
    //[assembly: RegisterGenericComponentType(typeof(ComponentType<DoorComponent>))]
    public int LevelTransition { private get; set; } = -1;

    private void Update()
    {
        if (LevelTransition != -1)
        {
            EntityManager entityManager = GetEntityManager();
            Entity e = GetEntity();
            entityManager.AddComponent<WallComponent>(e);

            entityManager.SetComponentData(e, new DoorComponent
            {
                levelTransition = LevelTransition,
                locked = true
            });
        }
    }

    private void SetTransition(int transitionValue, ref EntityManager manager, ref Entity e)
    {
        manager.SetComponentData(e, new DoorComponent
        {
            levelTransition = transitionValue,
            locked = true
        });
    }
}