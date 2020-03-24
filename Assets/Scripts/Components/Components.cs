using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public enum Team
    {
        Sheep = 0,
        Shepherd = 1,
        Enemy = 2
    }
    public enum Dir
    {
        North,
        East,
        South,
        West
    }

    public struct MovementComponent : IComponentData
    {
        public Dir currMovementDirection;
    }

    public struct TeamComponent : IComponentData
    {
        public Team team;
    }

    public struct ProjectileStatsComponent :IComponentData
    {
        public float DamageModifier;
        public float SpeedModifier;
        public bool Alive;
        public bool IsFromPlayer;
        public float3 Direction;
    }

    public struct VelocityComponent : IComponentData
    {
        public float Velocity;
    }

    public struct ColliderComponent : IComponentData
    {
        public float Size;
    }

    public struct DestructibleComponent : IComponentData
    {
        public bool Destroy;
    }

}