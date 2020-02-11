using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Components;
using EntityComponents;

public class EnvironmentUnit : MonoBehaviour
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;
    public Vector3 position = new Vector3(100, 100, 0);
    // Use this for initialization
    void Start()
    {
        print("Created EnvironmentUnit, Start");
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Rigidbody2D) // TODO : must implement
        );

        Entity e = entityManager.CreateEntity(entityArchetype);

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
