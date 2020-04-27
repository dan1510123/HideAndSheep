﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Components;
using EnvironmentComponents;
using System;

public class EnvironmentBehaviour<ComponentType> : MonoBehaviour
    where ComponentType : struct, IComponentData
{
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;

    private Vector3 position = new Vector3(100, 100, 0);
    private int transitionNumber = -1;
    private float scale = 0.5f;
    private Entity entity;
    EntityManager entityManager;

    public void SetPosition(Vector3 pos)
    {
        this.position = pos;
        transform.position = pos;
    }

    public EntityManager GetEntityManager()
    {
        return entityManager;
    }

    public Entity GetEntity()
    {
        return entity;
    }

    // Use this for initialization
    void Start()
    {
        //print("Created EnvironmentUnit, Start");
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(ComponentType),
            typeof(Scale),
            typeof(RenderMesh)
        );

        entity = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(entity, new Translation
        {
            Value = position
        });
        entityManager.SetComponentData(entity, new ColliderComponent
        {
            Size = 0.5f
        });
        entityManager.SetComponentData(entity, new Scale
        {
            Value = scale
        });
        entityManager.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = mesh,
            material = material

        });

        
    }

    private void Update()
    {
        entityManager.SetComponentData(entity, new Translation
        {
            Value = position
        });
    }
    //private void SetTransitionComponent(int transitionValue, ref EntityManager manager, Entity e)
    //{
    //    manager.SetComponentData(e, new DoorComponent
    //    {
    //        levelTransition = transitionValue
    //    });
    //}
}
