using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct MovingComponent : IComponentData 
{
    public float currMovementDirection;
}
