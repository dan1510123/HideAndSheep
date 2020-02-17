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
    public float scale = 1f;
    public Dir direction = Dir.East;

    private float damageModifier;
    public float speedModifier = 10f;

    private void Start()
    {
        print("Projectile instantiated");
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(ProjectileStatsComponent),
            typeof(MovementComponent),
            typeof(Translation),
            typeof(Scale),
            typeof(RenderMesh),
            typeof(ColliderComponent),
            typeof(DestructibleComponent),
            typeof(LocalToWorld)
        ); ;

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new ProjectileStatsComponent
        {
            SpeedModifier = speedModifier
        });
        entityManager.SetComponentData(e, new MovementComponent
        {
            currMovementDirection = this.direction
        });
        entityManager.SetComponentData(e, new Translation
        {
            Value = this.position
        });
        entityManager.SetComponentData(e, new Scale
        {
            Value = this.scale
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = this.scale
        });
        //entityManager.SetComponentData(e, new DestructibleComponent
        //{
        //    Destroy = false
        //});
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material
        });
    }

}