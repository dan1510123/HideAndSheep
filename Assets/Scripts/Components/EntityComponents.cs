using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace EntityComponents {
    public struct PlayerComponent : IComponentData
    {
        // Player 0 if being chased and 1 if chasing
        public int playerNumber;
    }
    public struct EnemyComponent : IComponentData
    {
        public int enemyType;
    }
    public struct ProjectileComponent : IComponentData
    {
    };


}