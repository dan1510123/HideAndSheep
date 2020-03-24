using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;
using ItemComponent;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    public float scale = 1f;

    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(ItemStats),
            typeof(StatsComponent)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new Translation
        {
            Value = new float3(-3, 3, 0)
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = 1f
        });
        entityManager.SetComponentData(e, new ItemStats
        {
            id = 0,
            type = ItemType.StatsItem
        });
        entityManager.SetComponentData(e, new StatsComponent
        {
            attack = 0,
            attackSpeed = 0,
            moveSpeed = 5,
            health = 5
        });

        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });

    }
}
