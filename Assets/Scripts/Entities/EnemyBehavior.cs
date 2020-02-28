using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(MovementComponent),
            typeof(WeaponComponent),
            typeof(StatsComponent),
            typeof(EnemyComponent)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new MovementComponent
        {
            currMovementDirection = 0
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = new float3(3, 3, 0)
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = 1f
        });
        entityManager.SetComponentData(e, new StatsComponent
        {
            attack = 10,
            attackSpeed = 0,
            moveSpeed = 0,
            health = 50
        });
        entityManager.SetComponentData(e, new EnemyComponent
        {
            enemyType = 0
        });

        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });

    }
}
