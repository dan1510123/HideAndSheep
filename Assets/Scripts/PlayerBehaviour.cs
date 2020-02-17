using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
    public float scale = 1f;

    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(WeaponComponent),
            typeof(PlayerStatsComponent),
            typeof(PlayerComponent),
            typeof(MovementComponent),
            typeof(Translation),
            typeof(Scale),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(VelocityComponent),
            typeof(ColliderComponent)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new PlayerStatsComponent
        {
            healthPoints = 100,
            damage = 1,
            speed = 1f
        });
        entityManager.SetComponentData(e, new MovementComponent
        {
            currMovementDirection = Dir.East
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = new Vector3(0, 0, 0)
        });
        entityManager.SetComponentData(e, new Scale
        {
            Value = scale
        });
        entityManager.SetComponentData(e, new VelocityComponent
        {
            Velocity = 5f
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = 1f
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });
    }
}
