using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LukensGenericUI
{
    public class InventoryManager : MonoBehaviour
    {
        public List<Slot> Slots;
        [SerializeField] private Transform m_SlotsParent;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            AddItem(Replicator.Instance.RequestItem("COINS", 50));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddItem(Replicator.Instance.RequestItem("COINS", 50));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddItem(Replicator.Instance.RequestItem("SHIELD_WOODEN", 3));
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddItem(Replicator.Instance.RequestItem("MEAT_RAW", 47));
            }
        }

        private void Initialize()
        {
            Slots = new();
            foreach (Transform child in m_SlotsParent)
            {
                Slots.Add(child.GetComponent<Slot>());
            }
        }
        public bool HasEmptySlot 
        { 
            get
            {
                bool empty = false;
                foreach (Slot slot in Slots)
                {
                    if (slot.IsEmpty)
                    {
                        empty = true;
                        break;
                    }
                }

                return empty;
            } 
        }

        public bool AddItem(InventoryItem itemToAdd)
        {
            if (itemToAdd.Amount == 0 || itemToAdd.ID == "")
            {
                return false;
            }

            bool successfullyAdded = false;
            bool inventoryAlreadyContainsItem = false;
            Slot foundSlot = null;

            foreach (Slot slot in Slots)
            {
                if (!slot.IsEmpty)
                {
                    if (slot.CurrentItem.ID.Equals(itemToAdd.ID) && !slot.IsFull)
                    {
                        inventoryAlreadyContainsItem = true;
                        foundSlot = slot;
                        break;
                    }
                }
            }

            if (inventoryAlreadyContainsItem)
            {
                if (foundSlot.CurrentItem.Amount + itemToAdd.Amount <= itemToAdd.MaxStack)
                {
                    foundSlot.SetAmount(foundSlot.CurrentItem.Amount + itemToAdd.Amount);
                    successfullyAdded = true;
                }
                else
                {
                    if (HasEmptySlot)
                    {
                        InventoryItem newLeftoverStack = foundSlot.CurrentItem.Clone();

                        newLeftoverStack.Amount = (foundSlot.CurrentItem.Amount + itemToAdd.Amount - itemToAdd.MaxStack);
                        foundSlot.SetAmount(foundSlot.CurrentItem.MaxStack);

                        AddItem(newLeftoverStack);
                        successfullyAdded = true;
                    }
                }
            }
            else
            {
                if (HasEmptySlot)
                {
                    if (itemToAdd.Amount < itemToAdd.MaxStack)
                    {
                        Slot emptySlot = GetFirstOpenSlot();
                        emptySlot.SetNewItem(itemToAdd);
                        successfullyAdded = true;
                    }
                    else
                    {
                        if (itemToAdd.MaxStack / itemToAdd.Amount < GetNumberOfEmptySlots())
                        {
                            InventoryItem leftoverItem = itemToAdd.Clone();
                            leftoverItem.Amount = itemToAdd.Amount - itemToAdd.MaxStack;

                            itemToAdd.Amount = itemToAdd.MaxStack;

                            Slot emptySlot = GetFirstOpenSlot();
                            emptySlot.SetNewItem(itemToAdd);

                            AddItem(leftoverItem);
                            successfullyAdded = true;
                        }
                    }
                }
            }

            if (successfullyAdded)
            {
                ReorderSlots();
            }

            return successfullyAdded;
        }

        private int GetNumberOfEmptySlots()
        {
            int counter = 0;

            foreach (Slot slot in Slots)
            {
                if (slot.IsEmpty)
                    counter++;
            }

            return counter;
        }

        private Slot GetFirstOpenSlot()
        {
            foreach (Slot slot in Slots)
            {
                if (slot.IsEmpty)
                    return slot;
            }

            Debug.LogError("Failed to find open slot: " + gameObject.name);
            return null;
        }

        private void ReorderSlots()
        {
            Slots.Sort(new Slot());

            List<Slot> emptySlots = new();
            List<Slot> filledSlots = new();

            foreach (Slot slot in Slots)
            {
                if (slot.IsEmpty)
                {
                    emptySlots.Add(slot);
                }
                else
                {
                    filledSlots.Add(slot);
                }
            }

            Slots.Clear();
            Slots = filledSlots;
            Slots.AddRange(emptySlots);
            
            //Slots.Reverse();

            for (int i = 0; i < Slots.Count; i++)
            {
                Slots[i].transform.SetSiblingIndex(i);
            }
        }
    }
}