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

    public static EntityArchetype GetArchetype()
    {
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
        );
        return entityArchetype;
    }
}