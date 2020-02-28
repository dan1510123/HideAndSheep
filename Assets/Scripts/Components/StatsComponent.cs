using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct StatsComponent : IComponentData
{
    public int attack;
    public int attackSpeed;
    public int moveSpeed;
    public int health;
}