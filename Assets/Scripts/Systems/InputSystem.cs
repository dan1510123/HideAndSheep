using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using EntityComponents;
using Unity.Mathematics;
using Unity.Rendering;

public class InputSystem : ComponentSystem
{
    private bool projectileFired = false;
    private float2 lastPlayerPosition = new float2(0, 0);
    private float timer;
    private GameObject player;
    private Animator playerAnimator;
    private Animation playerAnimation;
    private SpriteRenderer renderer;
    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isIdling;
    private bool isSideIdling;
    private bool isMovingSideways;
    private bool rendererFlipped;


    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation,
            ref PlayerComponent playerComponent,
            ref MovementComponent movementComponent,
            ref StatsComponent statsComponent,
            ref VelocityComponent velocityComponent,
            ref ColliderComponent colliderComponent) =>
        {
            player = GameObject.FindWithTag("Player");
            playerAnimator = player.GetComponent<Animator>();
            renderer = player.GetComponent<SpriteRenderer>();

            float s1 = colliderComponent.Size;
            float displacement = velocityComponent.Velocity * statsComponent.moveSpeed * Time.DeltaTime;

            #region keyups
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnimator.SetBool("isMovingUp", false);
                playerAnimator.SetBool("isBackIdling", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnimator.SetBool("isMovingSideways", false);
                playerAnimator.SetBool("isSideIdling", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnimator.SetBool("isMovingSideways", false);
                playerAnimator.SetBool("isSideIdling", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnimator.SetBool("isMovingDown", false);
                playerAnimator.SetBool("isIdling", true);
            }
            #endregion

            #region keydowns
            if (Input.GetKey(KeyCode.W))
            {
                playerAnimator.SetBool("isMovingUp", true);
                undoIdle(playerAnimator);
                float2 pos1 = new float2(translation.Value.x, translation.Value.y + displacement);
                float displacementWithCollision = DisplacementCheckCollision(pos1, s1, displacement);
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
                if (renderer.flipX == true)
                {
                    renderer.flipX = false;
                }
                undoIdle(playerAnimator);
                playerAnimator.SetBool("isMovingSideways", true);
                float2 pos1 = new float2(translation.Value.x - displacement, translation.Value.y);
                float displacementWithCollision = DisplacementCheckCollision(pos1, s1, displacement);
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
                undoIdle(playerAnimator);
                playerAnimator.SetBool("isMovingDown", true);
                float2 pos1 = new float2(translation.Value.x, translation.Value.y - displacement);
                float displacementWithCollision = DisplacementCheckCollision(pos1, s1, displacement);
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
                undoIdle(playerAnimator);
                playerAnimator.SetBool("isMovingSideways", true);
                if (!renderer.flipX)
                {
                    renderer.flipX = true;
                }
                float2 pos1 = new float2(translation.Value.x + displacement, translation.Value.y);
                float displacementWithCollision = DisplacementCheckCollision(pos1, s1, displacement);
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

            player.transform.position = translation.Value;

            timer += Time.DeltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                if (timer > 1)
                {
                    //Shoot projectile
                    float3 shootDir = Input.mousePosition;
                    shootDir.z = 0.0f;
                    shootDir = Camera.main.ScreenToWorldPoint(shootDir);
                    fireProjectile(movementComponent, translation, shootDir - translation.Value);
                }

            }
            //if (Input.GetKey(KeyCode.Space) && !projectileFired)
            //{
            //    fireProjectile(movementComponent, translation);
            //}
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //BRING UP THE ESCAPE MENU

            }
        });

        projectileFired = false;
        #endregion
    }

    void fireProjectile(MovementComponent movementComponent, Translation translation, float3 directionToPlayer)
    {
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
            IsFromPlayer = true,
            Direction = directionToPlayer

        });
        PostUpdateCommands.SetComponent(e, new MovementComponent
        {
            currMovementDirection = movementComponent.currMovementDirection
        });
        PostUpdateCommands.SetComponent(e, new Translation
        {
            Value = translation.Value
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

        projectileFired = true;
    }

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

    private void undoIdle(Animator animator)
    {
        animator.SetBool("isSideIdling", false);
        animator.SetBool("isIdling", false);
        animator.SetBool("isBackIdling", false);
    }
}
