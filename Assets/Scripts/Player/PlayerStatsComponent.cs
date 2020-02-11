using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct PlayerStatsComponent : IComponentData
{
    // Health
    public int healthPoints;
    // Damage this player can deal
    public int damage;
    // speed of movement
    public float speed;
}
