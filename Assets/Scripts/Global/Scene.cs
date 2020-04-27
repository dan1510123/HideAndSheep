using UnityEngine;
using ItemComponent;

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
    [SerializeField] public Sprite itemSpriteOne;

    private void Start()
    {
        // Set up static variables
        GlobalObjects.projectile = projectilePrefab;
        GlobalObjects.mesh = mesh;
        GlobalObjects.material = material;
        GlobalObjects.enemyPrefab = enemyPrefab;
        GlobalObjects.wallPrefab = wallPrefab;
        GlobalObjects.mapBehaviour = mapBehaviour;


        //Setting up the itemTable
        ItemID itemOneID;
        itemOneID.id = 0;
        itemOneID.type = 0;
        ItemStats itemOneStats;
        itemOneStats.moveSpeed = 2;
        itemOneStats.attack = 0;
        itemOneStats.attackSpeed = 0;
        itemOneStats.health = 0;
        Item itemOne = new Item();
        itemOne.itemSprite = itemSpriteOne;
        itemOne.itemID = itemOneID;
        itemOne.stats = itemOneStats;
        ItemTable table = new ItemTable();
        GlobalObjects.iTable = table;
        Debug.Log(table);
        Debug.Log(itemOne);
        table.addItem(itemOne);

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