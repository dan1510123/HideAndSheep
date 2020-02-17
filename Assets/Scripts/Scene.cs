using UnityEngine;

public class Scene : MonoBehaviour
{
    PlayerBehaviour player;
    [SerializeField] public PlayerBehaviour playerPrefab;
    [SerializeField] public ProjectileBehaviour projectilePrefab;
    [SerializeField] public MapGenerator mapGenerator;

    private void Start()
    {
        // Set up static variables
        StaticStuff.projectile = projectilePrefab;

        // Set up map
        mapGenerator.GenerateMap();
    }
}