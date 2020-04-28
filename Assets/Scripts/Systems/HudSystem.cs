using BackpackComponents;
using Components;
using EntityComponents;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudSystem : ComponentSystem
{
    // Start is called before the first frame update
    public Text healthText;

    public Image healthBar;
    public int healthTextSize = 400;
    public Text statText;
    public Text backPack;
    public static int backPackLeftTransform = 1000;
    public int numberOfItems = 0;
    public static int itemWidth = 25;
    private EntityManager entityManager;
    private bool sceneLoaded = false;
    private UnityEngine.SceneManagement.Scene currentScene;

    protected override void OnCreateManager()
    {
        base.OnCreateManager();
        entityManager = World.Active.EntityManager;
    }

    private void Start()
    {
    }

    protected override void OnUpdate()
    {
        if (!sceneLoaded)
        {
            currentScene = SceneManager.GetActiveScene();
            Debug.Log("Scene is not loaded");
            Debug.Log(currentScene.name);
            if (currentScene.name == "AnimationScene")
            {
                Debug.Log("Can we enter here?");
                sceneLoaded = true;
            }
        }
        else
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            healthBar = GameObject.Find("Image").GetComponent<Image>();
            backPack = GameObject.Find("UIBackPack").GetComponent<Text>();
            statText = GameObject.Find("StatText").GetComponent<Text>();
            //This Grabs everyone with these components
            //AKA the player
            Entities.ForEach((Entity e,
                ref PlayerComponent playerComponent,
                ref MovementComponent movementComponent,
                ref StatsComponent statsComponent,
                ref VelocityComponent velocityComponent,
                ref ColliderComponent colliderComponent) =>
            {
                //StatsComponent updateComponent = statsComponent;
                //Entities.ForEach((Entity f,
                //    ref HealthBarComponent healthBarComponent) =>
                //{
                //    healthBarComponent.health = updateComponent.health;
                //    healthBarComponent.attack = updateComponent.attack;
                //    healthBarComponent.attackSpeed = updateComponent.attackSpeed;
                //    healthBarComponent.moveSpeed = updateComponent.moveSpeed;
                //});
                healthText.text = "Current Health: " + statsComponent.health;
                healthBar.rectTransform.sizeDelta = new Vector2(healthTextSize * statsComponent.health / 100, 20);
                statText.text = "Attack Damage: " + statsComponent.attack + "\nAttack Speed: " + statsComponent.attackSpeed + "\nMove Speed: " + statsComponent.moveSpeed;
                DynamicBuffer<IntBufferElement> backpack = entityManager.GetBuffer<IntBufferElement>(e);
                backPack.text = "Backpack: ";
                foreach (var spriteValue in backpack.Reinterpret<IntBufferElement>())
                {
                    string gameObjectString = "Item" + spriteValue.value;
                    if (GameObject.Find(gameObjectString))
                    {
                        //Debug.Log("Already Generated");
                    }
                    else
                    {
                        float myScale = 1.5f;
                        GameObject itemIcon = new GameObject();
                        Image newImage = itemIcon.AddComponent<Image>();
                        itemIcon.GetComponent<RectTransform>().SetParent(backPack.transform);
                        itemIcon.transform.position = itemIcon.transform.position + new Vector3(backPackLeftTransform + (numberOfItems * itemWidth), 35, 0);
                        itemIcon.transform.localScale = new Vector3(myScale, myScale, myScale);
                        Debug.Log(itemIcon.transform.localPosition);
                        Item currentItem = GlobalObjects.iTable.lookupItem(spriteValue.value);
                        Sprite currentItemSprite = currentItem.itemSprite;
                        newImage.sprite = currentItemSprite;
                        itemIcon.name = "Item" + spriteValue.value;
                        numberOfItems++;
                    }
                    //backPack.text = backPack.text + " " + spriteValue.value;
                }
            });
        }
    }
}