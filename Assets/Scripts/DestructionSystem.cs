using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Components;
using EnvironmentComponents;
using EntityComponents;
using Unity.Mathematics;

//public class DestructionSystem : ComponentSystem
//{
//    private bool projectileFired = false;
//    private float2 lastPlayerPosition = new float2(0, 0);
//    protected override void OnUpdate()
//    {
//        Entities.ForEach((Entity e, ref DestructibleComponent destructibleComponent) =>
//        {
//            if(destructibleComponent.Destroy)
//            {
//                e.Destroy();
//            }
//        });
//    }
//}
