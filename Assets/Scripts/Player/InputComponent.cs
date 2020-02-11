using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct InputComponent : IComponentData
{
    // check that it's firing?
    public bool isFiring;
}
