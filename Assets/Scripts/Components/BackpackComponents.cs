using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Components;

namespace BackpackComponents
{
    public struct IntBufferElement : IBufferElementData
    {
        public int value;
    }
}