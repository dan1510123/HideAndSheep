using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using EntityComponents;
using Unity.Mathematics;

public class InputSystem : ComponentSystem
{
    private bool projectileFired = false;
    private float2 lastPlayerPosition = new float2(0, 0);
    protected override void OnUpdate()
    { 
        Entities.ForEach((ref Translation translation,
            ref PlayerComponent playerComponent,
            ref MovementComponent movementComponent,
            ref PlayerStatsComponent statsComponent,
            ref VelocityComponent velocityComponent,
            ref ColliderComponent colliderComponent) =>
        {
            float2 pos1 = new float2(translation.Value.x, translation.Value.y);
            float s1 = colliderComponent.Size;
            float displacement = velocityComponent.Velocity * statsComponent.speed * Time.DeltaTime;
            float displacementWithCollision = DisplacementCheckCollision(pos1, s1, displacement);

            if (Input.GetKey(KeyCode.W))
            {
                if (displacementWithCollision != 0)
                {
                    lastPlayerPosition.y = translation.Value.y;
                    translation.Value.y += displacementWithCollision;
                }
                else
                {
                    translation.Value.y = lastPlayerPosition.y;
                }
                movementComponent.currMovementDirection = Dir.North;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (displacementWithCollision != 0)
                {
                    lastPlayerPosition.x = translation.Value.x;
                    translation.Value.x -= displacementWithCollision;
                }
                else
                {
                    translation.Value.x = lastPlayerPosition.x;
                }
                movementComponent.currMovementDirection = Dir.West;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (displacementWithCollision != 0)
                {
                    lastPlayerPosition.y = translation.Value.y;
                    translation.Value.y -= displacementWithCollision;
                }
                else
                {
                    translation.Value.y = lastPlayerPosition.y;
                }
                movementComponent.currMovementDirection = Dir.South;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (displacementWithCollision != 0)
                {
                    lastPlayerPosition.x = translation.Value.x;
                    translation.Value.x += displacementWithCollision;
                }
                else
                {
                    translation.Value.x = lastPlayerPosition.x;
                }
                movementComponent.currMovementDirection = Dir.East;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !projectileFired)
            {
                // Shoot projectile
                ProjectileBehaviour projectile = Object.Instantiate(StaticStuff.projectile, translation.Value, Quaternion.identity);
                projectile.position = translation.Value;
                projectile.direction = movementComponent.currMovementDirection;
                projectile.scale = 0.5f;
                projectile.speedModifier = 10f;
                projectileFired = true;
            }
        });

        float DisplacementCheckCollision(float2 pos1, float s1, float displacement)
        {
            Entities.ForEach((ref WallComponent wallComponent,
                    ref Translation translation1,
                    ref ColliderComponent colliderComponent1) =>
            {
                float2 pos2 = new float2(translation1.Value.x, translation1.Value.y);
                float s2 = colliderComponent1.Size;
                bool collided = AreSquaresOverlapping(pos1, s1, pos2, s2);

                if (collided)
                {
                    displacement = 0;
                }
            });
            return displacement;
        }

        // Checks if the square at position posA and size sizeA overlaps 
        // with the square at position posB and size sizeB
        bool AreSquaresOverlapping(float2 posA, float sizeA, float2 posB, float sizeB)
        {
            float d = (sizeA / 2) + (sizeB / 2);
            return (math.abs(posA.x - posB.x) < d && math.abs(posA.y - posB.y) < d);
        }

        projectileFired = false;
    }

}
