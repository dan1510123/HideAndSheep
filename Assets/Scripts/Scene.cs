using UnityEngine;

public class Scene : MonoBehaviour
{
    PlayerBehaviour player;
    [SerializeField] public PlayerBehaviour playerPrefab;
    [SerializeField] public ProjectileBehaviour projectilePrefab;
    [SerializeField] public EnemyBehavior enemyPrefab;
    [SerializeField] public MapGenerator mapGenerator;
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;

    private void Start()
    {
        // Set up static variables
        GlobalObjects.projectile = projectilePrefab;
        GlobalObjects.mesh = mesh;
        GlobalObjects.material = material;

        // Set up map
        mapGenerator.GenerateMap();
    }
}