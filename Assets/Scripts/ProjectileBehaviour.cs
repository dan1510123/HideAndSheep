using System.Collections;
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
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
    public Vector3 position = new Vector3(0, 0, 0);
    public Dir direction = Dir.East;

    [SerializeField] private float damageModifier;
    [SerializeField] private float speedModifier;

    private void Start()
    {
        print("Projectile instantiated");
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
            currMovementDirection = this.direction
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = this.position
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });
    }

}