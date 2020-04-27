using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] public PlayerBehaviour playerPrefab;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject wallPrefab;
    [SerializeField] public ProjectileBehaviour projectilePrefab;
    [SerializeField] public ItemBehaviour itemPrefab;
    [SerializeField] public MapBehaviour mapBehaviour;
    [SerializeField] public Mesh mesh;
    [SerializeField] public Material material;

    private void Start()
    {
        // Set up static variables
        GlobalObjects.projectile = projectilePrefab;
        GlobalObjects.mesh = mesh;
        GlobalObjects.material = material;
        GlobalObjects.enemyPrefab = enemyPrefab;
        GlobalObjects.wallPrefab = wallPrefab;
        GlobalObjects.mapBehaviour = mapBehaviour;


        // Set up map
        GlobalObjects.mapLogic = new MapLogic();
        GlobalObjects.mapLogic.Setup(5);
        Room startRoom = GlobalObjects.mapLogic.currentFloorInfo.getStartRoom();
        GlobalObjects.mapBehaviour.Setup(ref startRoom);

        // Create Player
        PlayerBehaviour player = Instantiate(playerPrefab);
        player.SetSpawn(new Vector3(0, 0, 0));
    }
}