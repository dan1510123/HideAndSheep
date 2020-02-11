using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(WeaponComponent),
            typeof(InputComponent),
            typeof(PlayerStatsComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        ) ;

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new Translation
        {
            Value = new float3(0, 0, 0)
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });
        entityManager.SetComponentData(e, new PlayerStatsComponent
        {
            healthPoints = 100,
            damage = 1,
            speed = 2f
        }) ;

        
    }
}
