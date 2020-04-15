using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 spawn = new Vector3(2, 1, 0);

    //[SerializeField] private Mesh mesh;
    //[SerializeField] private Material material;

    private GameObject player;
    private GameObject enemyGameObject;
    private Vector2 target;
    private Entity entity;
    private EntityManager entityManager;
    private BoxCollider2D collider;
    private Vector2 lastDir;
    private SpriteRenderer spriteRenderer;

    public void setSpawn(Vector3 newSpawn)
    {
        spawn = newSpawn;
    }

    private void Start()
    {

        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyGameObject = transform.gameObject;

        player = GameObject.FindWithTag("Player");

        entityManager = World.Active.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(ColliderComponent),
            typeof(MovementComponent),
            typeof(WeaponComponent),
            typeof(StatsComponent),
            typeof(EnemyComponent)
        );

        entity = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(entity, new MovementComponent
        {
            currMovementDirection = 0
        });
        entityManager.SetComponentData(entity, new Translation
        {
            Value = spawn
        });
        transform.position = spawn;
        entityManager.SetComponentData(entity, new ColliderComponent
        {
            Size = 1f
        });
        entityManager.SetComponentData(entity, new StatsComponent
        {
            attack = 10,
            attackSpeed = 0,
            moveSpeed = 0,
            health = 50
        });
        entityManager.SetComponentData(entity, new EnemyComponent
        {
            enemyType = 0
        });
        //entityManager.SetSharedComponentData(entity, new RenderMesh
        //{
        //    mesh = mesh,
        //    material = material

        //});
    }

    void Update()
    {
        target = player.transform.position;
        Vector2 dir = AttemptMove(transform.position, target);
        lastDir = dir;
        Vector2 currPos = transform.position;
        currPos += dir * 0.01f;
        transform.position = currPos;

        entityManager.SetComponentData(entity, new Translation
        {
            Value = transform.position
        });

        int health = entityManager.GetComponentData<StatsComponent>(entity).health;
        if(health <= 0)
        {
            Destroy(enemyGameObject);
        }
    }

    protected bool Move(Vector2 startPos, Vector2 endPos, out RaycastHit2D hit)
    {
        collider.enabled = false;
        Vector2 dir =  endPos - startPos;
        dir.Normalize();
        hit = Physics2D.Linecast(startPos, endPos, LayerMask.GetMask("Wall"));
        //hit = Physics2D.BoxCast(startPos, new Vector2(1, 1), 0f, dir, (endPos - startPos).magnitude, LayerMask.GetMask("Wall"));

        collider.enabled = true;
       
        if (hit.transform == null)
        {
            return true;
        }
        else
        {
            //Debug.Log("hit transform: " + hit.collider.gameObject + " " + hit.collider.transform.position);
            return false;
        }
    }

    protected Vector2 AttemptMove(Vector2 start, Vector2 target)
    {
        RaycastHit2D hit;
        bool ableToMove = Move(start, target, out hit);

        if (hit.transform == null)
        {
            Vector2 directionToEnd = target - start;
            directionToEnd.Normalize();
            //Debug.Log("target: " + target + "curr: " + transform.position);
            return directionToEnd;
        }
        else
        {
            // wonder around randomly
            return WonderAround(start);
        }


    }

    protected Vector2 WonderAround(Vector2 startPos)
    {
        RaycastHit2D hit;
        bool ableToMove = Move(startPos, startPos + lastDir, out hit);

        if (hit.transform == null)
        {
            //Debug.Log("last dir" + lastDir);
            return lastDir;
        }
        else
        {
            Vector2 moveDir;
            float angle = UnityEngine.Random.Range(-110, 110);
            float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

            moveDir.x = (cos * 1) - (sin * 0);
            moveDir.y = (sin * 1) + (cos * 0);
         
            //moveDir.Normalize();
            //Debug.Log("move dir" + moveDir);
            return moveDir;
        }
    }

}
