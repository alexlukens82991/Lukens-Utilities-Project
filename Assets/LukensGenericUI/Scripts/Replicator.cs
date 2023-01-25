using LukensUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : Singleton<Replicator>
{
    public TemplateInventoryItem[] m_InvetoryItemTemplates;

    
    public new void Awake()
    {
        base.Awake();
        GetInventoryItemsFromFolder();
    }

    private void GetInventoryItemsFromFolder()
    {
        // Get all scriptableobjects in a folder in Unity
        m_InvetoryItemTemplates = Resources.LoadAll<TemplateInventoryItem>("Inventory/Templates");
    }

    public InventoryItem RequestItem(string ID, int amount)
    {
        TemplateInventoryItem foundItem = FindItem(ID);

        if (foundItem == null)
        {
            return null;
        }

        return foundItem.CreateInventoryItem(amount);
    }

    private TemplateInventoryItem FindItem(string ID)
    {
        foreach (TemplateInventoryItem item in m_InvetoryItemTemplates)
        {
            if (item.ID.Equals(ID))
                return item;
        }

        Debug.LogError("Could not find scriptableobject in replicator: " + ID);
        return null;
    }
}
