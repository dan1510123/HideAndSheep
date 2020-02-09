using UnityEngine;
using System.Collections;
using Components;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float damageModifier;
    [SerializeField] private float speedModifier;

    public MovementComponent movementComponent;
    public TeamComponent teamComponent;
    public StatsComponent statsComponent;
    public CollisionComponent collisionComponent;
    public ProjectileBehaviour(int damageModifier)
    {
        this.damageModifier = damageModifier;
    }
}