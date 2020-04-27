using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;
using Components;
using EntityComponents;

public struct GlobalObjects
{
    [SerializeField] public static ProjectileBehaviour projectile;
    [SerializeField] public static Mesh mesh;
    [SerializeField] public static Material material;
    public static GameObject enemyPrefab;
    public static GameObject wallPrefab;
    public static MapBehaviour mapBehaviour;
    public static MapLogic mapLogic;
    public static Vector2 cameraPosition = new Vector2(0, 0);
    [SerializeField] public static ItemTable iTable;
}
