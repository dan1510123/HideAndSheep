using UnityEngine;

public class Scene : MonoBehaviour
{
    PlayerBehaviour player;
    [SerializeField] Mesh playerMesh;
    [SerializeField] Material material;
    [SerializeField] public PlayerBehaviour playerPrefab;
    [SerializeField] public ProjectileBehaviour projectilePrefab;
    [SerializeField] public EnvironmentUnit environmentUnit;

    private void Start()
    {
        StaticStuff.projectile = projectilePrefab;
        // Create Player
        player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        player.scale = 0.75f;
    }
}