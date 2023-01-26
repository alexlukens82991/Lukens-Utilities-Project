using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using LukensGenericUI;

[Serializable]
public class InventoryItem
{
    [JsonProperty]
    public string ID;

    [JsonProperty]
    public int Amount;

    [JsonProperty]
    public int MaxStack;

    [JsonProperty]
    public Sprite Sprite;

    [JsonProperty]
    public Slot Slot; 

    public InventoryItem Clone()
    {
        InventoryItem newItem = new();

        newItem.ID = ID;
        newItem.Amount = Amount;
        newItem.MaxStack = MaxStack;
        newItem.Sprite = Sprite;

        return newItem;
    }

    public void ClearItem()
    {
        ID = "";
        Amount = 0;
        MaxStack = 1;
        Sprite = null;
    }
}
