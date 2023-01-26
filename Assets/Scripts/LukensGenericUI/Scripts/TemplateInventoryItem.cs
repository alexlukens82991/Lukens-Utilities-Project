using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTemplate", menuName = "Lukens/Inventory/ScriptableObjects")]
public class TemplateInventoryItem : ScriptableObject
{
    public string ID;

    public int MaxStack;

    public Sprite Sprite;

    public InventoryItem CreateInventoryItem(int amount)
    {
        InventoryItem newItem = new();

        newItem.ID = ID;
        newItem.Amount = amount;
        newItem.MaxStack = MaxStack;
        newItem.Sprite = Sprite;

        return newItem;
    }
}
