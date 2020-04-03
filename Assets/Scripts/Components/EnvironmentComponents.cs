using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace EnvironmentComponents
{
    public struct WallComponent : IComponentData {}

    public struct DoorComponent : IComponentData {
        public int levelTransition;
    }
}
