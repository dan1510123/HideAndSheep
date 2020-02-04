﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class InputSystem : ComponentSystem
{
    
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation) =>
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                translation.Value.y += 3f;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                translation.Value.x -= 3f;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                translation.Value.y -= 3f;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                translation.Value.x += 3f;
            }
        });
        
    }
}
