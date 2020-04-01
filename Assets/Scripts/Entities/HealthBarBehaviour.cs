using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Unity.Entities;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Text healthBar;
    Entity e;
    EntityManager entityManager;
    void Start()
    {
        healthBar.text = "DEFAULT HEALTH BAR";
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //EntityArchetype entityArchetype = entityManager.CreateArchetype(
        //   typeof(StatsComponent),
        //    );
        //e = entityManager.CreateEntity(entityArchetype);
        //entityManager.SetComponentData(e, new HealthBarComponent
        //{
        //    attack = 1,
        //    attackSpeed = 1,
        //    moveSpeed = 1,
        //    health = 10,
        //});
        
    }

    private void Update()
    {
    }
}
