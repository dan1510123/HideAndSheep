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
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            EntityArchetype entityArchetype = entityManager.CreateArchetype(
                typeof(Translation),
                typeof(LocalToWorld),
                typeof(ColliderComponent),
                typeof(DoorComponent),
                typeof(Scale),
                typeof(RenderMesh)
            );

            Entity e = entityManager.CreateEntity(entityArchetype);
            
            entityManager.SetComponentData(e, new Translation
            {
                Value = new Vector3(-9999, -9999)
            });
            SetTransition(LevelTransition, ref entityManager, ref e);

        }
    }

    private void SetTransition(int transitionValue, ref EntityManager manager, ref Entity e)
    {
        manager.SetComponentData(e, new DoorComponent
        {
            levelTransition = transitionValue
        });
    }
}