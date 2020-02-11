using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace EnvironmentComponents
{
    // Designating whether or not player collides
    public enum WallComponent
    {
        Collideable = 0, //player can't walk through
        Scenery = 1     //player can walk through
    }

    public struct TranslationComponent : IComponentData
    {
        public Transform startingPosition; //Where it starts
        public float3 movement;            //movespeeds
    }

    public struct CollisionComponent : IComponentData
    {
        //TODO
    }
}
