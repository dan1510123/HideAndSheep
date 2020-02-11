using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct PlayerComponent : IComponentData
{
    // Player 0 if being chased and 1 if chasing
    public int playerNumber;
}
