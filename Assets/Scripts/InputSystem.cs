using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EntityComponents;

public class InputSystem : ComponentSystem
{
    private bool projectileFired = false;
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation,
            ref PlayerComponent playerComponent,
            ref MovementComponent movementComponent,
            ref PlayerStatsComponent statsComponent) =>
        {
            if (Input.GetKey(KeyCode.W))
            {
                translation.Value.y += statsComponent.speed * Time.DeltaTime;
                movementComponent.currMovementDirection = Dir.North;
            }
            if (Input.GetKey(KeyCode.A))
            {
                translation.Value.x -= statsComponent.speed * Time.DeltaTime;
                movementComponent.currMovementDirection = Dir.West;
            }
            if (Input.GetKey(KeyCode.S))
            {
                translation.Value.y -= statsComponent.speed * Time.DeltaTime;
                movementComponent.currMovementDirection = Dir.South;
            }
            if (Input.GetKey(KeyCode.D))
            {
                translation.Value.x += statsComponent.speed * Time.DeltaTime;
                movementComponent.currMovementDirection = Dir.East;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !this.projectileFired)
            {
                // Shoot projectile
                ProjectileBehaviour projectile = Object.Instantiate(StaticStuff.projectile, translation.Value, Quaternion.identity);
                projectile.position = translation.Value;
                projectile.direction = movementComponent.currMovementDirection;
                projectile.scale = 0.5f;
                projectile.speedModifier = 10f;
                this.projectileFired = true;
            }
        });
        this.projectileFired = false;
    }

}
