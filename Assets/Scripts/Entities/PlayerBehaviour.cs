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
    private float scale = 0.5f;
    private Vector3 spawn = new Vector3(110, 110, 0);

    public void SetSpawn(Vector3 pos)
    {
        spawn = pos;
    }

    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(WeaponComponent),
            typeof(StatsComponent),
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

        entityManager.SetComponentData(e, new StatsComponent
        {
            attack = 1,
            attackSpeed = 1,
            moveSpeed = 1,
            health = 100
        });
        entityManager.SetComponentData(e, new MovementComponent
        {
            currMovementDirection = Dir.East
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = spawn
        });
        entityManager.SetComponentData(e, new Scale
        {
            Value = scale
        });
        entityManager.SetComponentData(e, new VelocityComponent
        {
            Velocity = 2f
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = 0.5f
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });

    }
}
