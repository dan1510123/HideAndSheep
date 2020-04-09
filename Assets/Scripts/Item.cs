using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemComponent;

public struct Item
{
    [SerializeField] public ItemID itemID;
    [SerializeField] public Sprite itemSprite;
    public ItemStats stats;
}
