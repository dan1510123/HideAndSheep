﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(ProjectileComponent),
            typeof(MovementComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new MovementComponent
        {
            currMovementDirection = Dir.East
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = new float3(0, 0, 0)
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });
    }

    [SerializeField] private float damageModifier;
    [SerializeField] private float speedModifier;

    public MovementComponent movementComponent;
    public TeamComponent teamComponent;
    public StatsComponent statsComponent;
    public CollisionComponent collisionComponent;
    public ProjectileBehaviour(float damageModifier, Team team, StatsComponent s)
    {
        this.damageModifier = damageModifier;
    }
}