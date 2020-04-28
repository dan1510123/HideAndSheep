using EntityComponents;
using EnvironmentComponents;
using Unity.Entities;
using UnityEngine;

public class RoomSystem : ComponentSystem
{
    EntityManager entityManager;
    Camera camera;

    protected override void OnCreateManager()
    {
        base.OnCreateManager();

        entityManager = World.Active.EntityManager;
        camera = GameObject.FindObjectOfType<Camera>();
    }
    protected override void OnUpdate()
    {
        int enemiesLeft = 0;
        Entities.ForEach((Entity e,
            ref EnemyComponent enemyComponent) =>
        {
            enemiesLeft++;
        });

        if (enemiesLeft == 0)
        {
            Entities.ForEach((Entity e,
            ref DoorComponent doorComponent,
            ref WallComponent wallComponent) =>
            {
                doorComponent.locked = false;
            });

            if(GlobalObjects.mapLogic.currentRoom.finalRoom)
            {
                Debug.Log("GAME OVER WIN");
                Entities.ForEach((Entity e) =>
                {
                    PostUpdateCommands.DestroyEntity(e);
                });
                Application.LoadLevel(3);
            }
        }

        Entities.ForEach((Entity e,
            ref DoorComponent doorComponent,
            ref WallComponent wallComponent) =>
        {
            if(doorComponent.locked == false)
            {
                entityManager.RemoveComponent<WallComponent>(e);
            }
        });

    }
}
