using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class CollisionSystem : ComponentSystem
{
    public void checkColision<T, K>(ref T collider1, ref K collider2) 
        where T: IComponentData 
        where K : IComponentData
    {

    }
    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
