using UnityEngine;
using UnityEditor;
using ItemComponent;
using System.Collections.Generic;

public class ItemTable
{
    [SerializeField] public Dictionary<int, Item> itemTable;
    ItemID missingNoId;
    ItemStats missingNoStats;
    Item missingNoItem;
    public ItemTable()
    {
        missingNoId.id = -1;
        missingNoId.type = 0;
        missingNoStats.attack = 0;
        missingNoStats.attackSpeed = 0;
        missingNoStats.moveSpeed = 0;
        missingNoStats.health = 0;
        missingNoItem.itemID = missingNoId;
        missingNoItem.stats = missingNoStats;
        missingNoItem.itemSprite = null;
        itemTable = new Dictionary<int, Item>();
    }

    public void addItem(Item insertedItem)
    {
        if(!itemTable.ContainsKey(insertedItem.itemID.id))
        {
            itemTable.Add(insertedItem.itemID.id, insertedItem);
        }
    }

    public Item lookupItem(int itemId)
    {
        Item returnItem;
        if( itemTable.TryGetValue(itemId, out returnItem))
        {
            return returnItem;
        }
        else
        {
            return missingNoItem;
        }
    }
}