using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Components;
using EntityComponents;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

public class EnemySystem : ComponentSystem
{
    Entity playerEntity;
    float3 playerPos;
    float timer = 0;
    bool playerAlive;

    protected override void OnUpdate()
    {
        playerAlive = false;
        // get the player's information
        Entities.ForEach((Entity player,
            ref PlayerComponent playerComponent,
            ref Translation translation) =>
        {
            playerEntity = player;
            playerAlive = true;
            playerPos = translation.Value;
        });

        Entities.ForEach((
            ref EnemyComponent enemyComponent,
            ref Translation translation
            ) =>
        {
            timer += Time.DeltaTime;
            if (timer > 3 && playerAlive)
            {
                Shoot(translation.Value, playerPos);
                timer = 0;
            }
        });

    }

    void Shoot(float3 enemyPos, float3 playerPos)
    {
        float3 directionToPlayer = playerPos - enemyPos;
        Vector2 dirNormalize;
        dirNormalize.x = directionToPlayer.x;
        dirNormalize.y = directionToPlayer.y;
        dirNormalize.Normalize();

        directionToPlayer.x = dirNormalize.x;
        directionToPlayer.y = dirNormalize.y;

        Entity e = PostUpdateCommands.CreateEntity(ProjectileBehaviour.GetArchetype());

        PostUpdateCommands.SetComponent(e, new ProjectileStatsComponent
        {
            SpeedModifier = 10f,
            Alive = true,
            IsFromPlayer = false,
            Direction = directionToPlayer
        });

        PostUpdateCommands.SetComponent(e, new Translation
        {
            Value = enemyPos
        });
        PostUpdateCommands.SetComponent(e, new Scale
        {
            Value = 0.4f
        });
        PostUpdateCommands.SetComponent(e, new ColliderComponent
        {
            Size = .2f
        });
        PostUpdateCommands.SetSharedComponent(e, new RenderMesh
        {
            mesh = GlobalObjects.mesh,
            material = GlobalObjects.material
        });
    }

    void DistanceToPlayer()
    {

    }


}
