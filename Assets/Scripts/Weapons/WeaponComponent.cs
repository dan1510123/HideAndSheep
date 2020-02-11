using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct WeaponComponent : IComponentData
{
    // we need a weapon type enum??
    public WeaponType weaponType;
    public int projectileNumber;
    public int projectileType;
}
