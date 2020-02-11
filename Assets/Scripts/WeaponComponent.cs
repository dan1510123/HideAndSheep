using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct WeaponComponent : IComponentData
{
    public int weaponType;
    public int projectileNumber;
    public int projectileType;
}