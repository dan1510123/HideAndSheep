using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] public PlayerBehaviour playerPrefab;
    [SerializeField] public ProjectileBehaviour projectilePrefab;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public ItemBehaviour itemPrefab;
    [SerializeField] public MapBehaviour mapGenerator;
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;

    private void Start()
    {
        // Set up static variables
        GlobalObjects.projectile = projectilePrefab;
        GlobalObjects.mesh = mesh;
        GlobalObjects.material = material;

        // Set up map
        mapGenerator.Setup();

        // Create Player
        PlayerBehaviour player = Instantiate(playerPrefab);
        player.SetSpawn(new Vector3(0, 0, 0));


    }
}