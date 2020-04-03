using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Components;

namespace ItemComponent
{
    public struct ItemID : IComponentData
    {
        public int id;
        public ItemType type;
    }

    public struct ItemStats : IComponentData
    {
        public int attack;
        public int attackSpeed;
        public int moveSpeed;
        public int health;
    }
}