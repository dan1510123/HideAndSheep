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
        Entities.ForEach((ref Translation translation, ref PlayerComponent playerComponent, ref MovementComponent movementComponent) =>
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                translation.Value.y += 1f;
                movementComponent.currMovementDirection = Dir.North;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                translation.Value.x -= 1f;
                movementComponent.currMovementDirection = Dir.West;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                translation.Value.y -= 1f;
                movementComponent.currMovementDirection = Dir.South;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                translation.Value.x += 1f;
                movementComponent.currMovementDirection = Dir.East;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !this.projectileFired)
            {
                // Shoot projectile
                ProjectileBehaviour projectile = Object.Instantiate(StaticStuff.projectile, translation.Value, Quaternion.identity);
                projectile.position = translation.Value;
                projectile.direction = movementComponent.currMovementDirection;
                projectile.scale = 0.5f;
                this.projectileFired = true;
            }
        });
        this.projectileFired = false;
    }
}
