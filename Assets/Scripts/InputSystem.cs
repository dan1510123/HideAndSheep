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
<<<<<<< HEAD
            if (Input.GetKeyDown(KeyCode.W))
            {
                translation.Value.y += 1f;
                movementComponent.currMovementDirection = Dir.North;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                translation.Value.x -= 1f;
                movementComponent.currMovementDirection = Dir.West;
=======
            // detect the end of movement
            //if (Input.GetKeyUp(KeyCode.W))
            //{
            //    isMoving = false;
            //}
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    isMoving = true;
            //    direction = 0;
            //}
            //if (Input.GetKeyUp(KeyCode.A))
            //{
            //    isMoving = false;
            //}
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    isMoving = true;
            //    direction = 1;
            //}
            //if (Input.GetKeyUp(KeyCode.S))
            //{
            //    isMoving = false;
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    isMoving = true;
            //    direction = 2;
            //}
            //if (Input.GetKeyUp(KeyCode.D))
            //{
            //    isMoving = false;
            //}
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    isMoving = true;
            //    direction = 3;
            //}
            if (Input.GetKey(KeyCode.W))
            {
                translation.Value.y += statsComponent.speed * Time.DeltaTime;
>>>>>>> 63c6866... Fluid motion with keypress
            }
            if (Input.GetKey(KeyCode.A))
            {
<<<<<<< HEAD
                translation.Value.y -= 1f;
                movementComponent.currMovementDirection = Dir.South;
=======
                translation.Value.x -= statsComponent.speed * Time.DeltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                translation.Value.y -= statsComponent.speed * Time.DeltaTime;
>>>>>>> 63c6866... Fluid motion with keypress
            }
            if (Input.GetKey(KeyCode.D))
            {
<<<<<<< HEAD
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
=======
                translation.Value.x += statsComponent.speed * Time.DeltaTime;
            }

            // detect the start of movement





            //// move the character
            //if (isMoving && direction == 0)
            //{
            //    translation.Value.y += statsComponent.speed * Time.DeltaTime;
            //}
            //if (isMoving && direction == 1)
            //{
            //    translation.Value.x -= statsComponent.speed * Time.DeltaTime;
            //}
            //if (isMoving && direction == 2)
            //{
            //    translation.Value.y -= statsComponent.speed * Time.DeltaTime;
            //}
            //if (isMoving && direction == 3)
            //{
            //    translation.Value.x += statsComponent.speed * Time.DeltaTime;
            //}
>>>>>>> 63c6866... Fluid motion with keypress

        });
        this.projectileFired = false;
    }

}
