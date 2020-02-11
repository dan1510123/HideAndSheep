using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace EnvironmentComponents
{
    // Designating whether or not player collides
    public enum EnvType
    {
        Collideable = 0, //player can't walk through
        Scenery = 1     //player can walk through
    }

    public struct WallComponent : IComponentData
    {
        public EnvType envType;
    }
}
