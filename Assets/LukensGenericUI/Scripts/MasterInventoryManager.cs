using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukensGenericUI
{
    public class MasterInventoryManager : MonoBehaviour
    {
        [Header("Current")]
        [SerializeField] private InventoryManager m_CurrentOpenInventory;

        [Header("Player")]
        [SerializeField] private InventoryManager m_PlayerInventory;

        [Header("Cache")]
        [SerializeField] private InventoryManager m_TestingInventory;


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                if (m_PlayerInventory.IsOpen)
                {
                    m_PlayerInventory.CloseInventory();
                }
                else
                {
                    OpenInventory(m_PlayerInventory);
                }
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                OpenInventory(m_TestingInventory);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAllInventories();
            }
        }

        public void OpenInventory(InventoryManager inventory)
        {
            m_PlayerInventory.OpenInventory();

            if (inventory != m_PlayerInventory)
            {
                inventory.OpenInventory();

                inventory.ConnectedInventory = m_PlayerInventory;
                m_PlayerInventory.ConnectedInventory = inventory;

                m_CurrentOpenInventory = inventory;
            }
        }

        public void CloseAllInventories()
        {
            m_PlayerInventory.CloseInventory();
            m_CurrentOpenInventory.CloseInventory();
        }
    }
}