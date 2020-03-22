using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Components;
using EnvironmentComponents;

public class EnvironmentBehaviour : MonoBehaviour
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
    public Vector3 position = new Vector3(100, 100, 0);
    // Use this for initialization
    void Start()
    {
        //print("Created EnvironmentUnit, Start");
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(WallComponent),
            typeof(RenderMesh)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new Translation
        {
            Value = position
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
