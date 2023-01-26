using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using LukensUtils;
using UnityEngine.UI;

namespace LukensGenericUI
{
    public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IComparer<Slot>
    {
        public bool IsEmpty { get { return CurrentItem.Amount == 0; } }
        public bool IsFull { get { return CurrentItem.Amount == CurrentItem.MaxStack; } }

        public InventoryItem CurrentItem;

        public InventoryManager ControllingManager;

        [SerializeField] private bool m_Primed;

        [Header("Cache")]
        [SerializeField] private Sprite m_DefaultEmptySprite;
        [SerializeField] private CanvasGroup m_AmountPanel;
        [SerializeField] private TextMeshProUGUI m_AmountTxt;
        [SerializeField] private Image m_DisplaySprite;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            UpdateUI();
        }

        private void Initialize()
        {
            ControllingManager = GetComponentInParent<InventoryManager>();
        }

        private void UpdateUI()
        {
            if (CurrentItem.Amount > 0)
            {
                LukensUtilities.ToggleCanvasGroup(m_AmountPanel, true);
                m_AmountTxt.text = CurrentItem.Amount.ToString();

                if (m_DisplaySprite.sprite != CurrentItem.Sprite)
                    m_DisplaySprite.sprite = CurrentItem.Sprite;
            }
            else
            {
                LukensUtilities.ToggleCanvasGroup(m_AmountPanel, false);
            }

        }

        public void SetNewItem(InventoryItem item)
        {
            CurrentItem = item.Clone();
            UpdateUI();
        }

        public void SetAmount(int amount)
        {
            CurrentItem.Amount = amount;
            UpdateUI();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Primed = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Primed)
                m_Primed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!m_Primed)
                return;

            print(gameObject.name + " CLICKED!");
            AttemptTransferItem();
            m_Primed = false;
        }

        public void ClearItem()
        {
            CurrentItem.ClearItem();
            m_DisplaySprite.sprite = m_DefaultEmptySprite;
            UpdateUI();
        }

        private void AttemptTransferItem()
        {
            ControllingManager.TransferItemToConnectedInventory(this);
        }


        public int Compare(Slot x, Slot y)
        {
            if (x.CurrentItem.ID != y.CurrentItem.ID)
            {
                return string.Compare(x.CurrentItem.ID, y.CurrentItem.ID, System.StringComparison.Ordinal);
            }
            else
            {
                return x.CurrentItem.Amount > y.CurrentItem.Amount ? -1 : 1;
            }
        }
    }
}