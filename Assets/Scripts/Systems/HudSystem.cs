using System.Collections;
using System.Collections.Generic;
using Components;
using EnvironmentComponents;
using BackpackComponents;
using EntityComponents;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HudSystem : ComponentSystem
{
    // Start is called before the first frame update
    public Text healthText;
    public Image healthBar;
    public Text statText;
    public Text backPack;
    public static int backPackLeftTransform = 500;
    public int numberOfItems = 0;
    public static int itemWidth = 50;

    EntityManager entityManager;

    protected override void OnCreateManager()
    {
        base.OnCreateManager();

        entityManager = World.Active.EntityManager;
    }
    void Start()
    {
    }
    protected override void OnUpdate()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthBar = GameObject.Find("Image").GetComponent<Image>();
        statText = GameObject.Find("StatText").GetComponent<Text>();
        backPack = GameObject.Find("UIBackPack").GetComponent<Text>();
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                statsComponent.health--;
                Debug.Log("The Current Health is decremented to " + statsComponent.health);

            }
        healthText.text = "Current Health: " + statsComponent.health;
        healthBar.rectTransform.sizeDelta = new Vector2(statsComponent.health * 40, 20);
        statText.text = "Attack Damage: " + statsComponent.attack + "\nAttack Speed: " + statsComponent.attackSpeed + "\nMove Speed: " + statsComponent.moveSpeed;
        DynamicBuffer<IntBufferElement> backpack = entityManager.GetBuffer<IntBufferElement>(e);
        backPack.text = "Backpack: ";
        foreach (var spriteValue in backpack.Reinterpret<IntBufferElement>())
        {
            string gameObjectString = "Item" + spriteValue.value;
            if (GameObject.Find(gameObjectString))
            {
                Debug.Log("Already Generated");
            }
            else
            {
                GameObject itemIcon = new GameObject();
                Image newImage = itemIcon.AddComponent<Image>();
                itemIcon.GetComponent<RectTransform>().SetParent(backPack.transform);
                itemIcon.transform.position = itemIcon.transform.position + new Vector3(backPackLeftTransform + numberOfItems * itemWidth, 50, 0);
                Debug.Log(itemIcon.transform.localPosition);
                Item currentItem = GlobalObjects.iTable.lookupItem(spriteValue.value);
                Sprite currentItemSprite = currentItem.itemSprite;
                newImage.sprite = currentItemSprite;
                itemIcon.name = "Item" + spriteValue.value;
                numberOfItems++;
            }
                backPack.text = backPack.text + " " + spriteValue.value;
                
            }
        });

    }
}
