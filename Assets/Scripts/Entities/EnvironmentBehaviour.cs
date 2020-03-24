using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Components;
using EnvironmentComponents;

public class EnvironmentBehaviour<ComponentType> : MonoBehaviour
    where ComponentType : struct, IComponentData
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
    private Vector3 position = new Vector3(100, 100, 0);
    private float scale = 0.5f;

    public void SetPosition(Vector3 pos)
    {
        this.position = pos;
    }

    // Use this for initialization
    void Start()
    {
        print("Created EnvironmentUnit, Start");
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(ComponentType),
            typeof(Scale),
            typeof(RenderMesh)
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(e, new Translation
        {
            Value = position
        });
        entityManager.SetComponentData(e, new ColliderComponent
        {
            Size = 0.5f
        });
        entityManager.SetComponentData(e, new Scale
        {
            Value = scale
        });
        entityManager.SetSharedComponentData(e, new RenderMesh
        {
            mesh = mesh,
            material = material

        });
    }
}
