using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

namespace Inventory.UI
{
    public class InventorySystem : MonoBehaviour

    {
        public GameObject Tab;
        
        
            [SerializeField]
            private UIInventoryItem itemPrefab;

            [SerializeField]
            private RectTransform contentPanel;

            [SerializeField]
            private UIInventoryDescription itemDescription;

            

            List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

            private int currentlyDraggedItemIndex = -1;

            public event Action<int> OnDescriptionRequested,
                    OnItemActionRequested,
                    OnStartDragging;

            public event Action<int, int> OnSwapItems;

            [SerializeField]
            

            private void Awake()
            {
                Hide();
                
                itemDescription.ResetDescription();
            }

            public void InitializeInventoryUI(int inventorysize)
            {
                for (int i = 0; i < inventorysize; i++)
                {
                    UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);    
                    uiItem.transform.SetParent(contentPanel);
                    listOfUIItems.Add(uiItem);
                    uiItem.OnItemClicked += HandleItemSelection;
                    uiItem.OnItemBeginDrag += HandleBeginDrag;
                    uiItem.OnItemDroppedOn += HandleSwap;
                    uiItem.OnItemEndDrag += HandleEndDrag;
                    uiItem.OnRightMouseBtnClick += HandleShowItemActions;
                }
            }

            internal void ResetAllItems()
            {
                foreach (var item in listOfUIItems)
                {
                    item.ResetData();
                    item.Deselect();
                }
            }

            internal void UpdateDescription(int itemIndex, Sprite itemImage, string description)
            {
                itemDescription.SetDescription(itemImage, description);
                DeselectAllItems();
                listOfUIItems[itemIndex].Select();
            }

            public void UpdateData(int itemIndex,
                Sprite itemImage, int itemQuantity)
            {
                if (listOfUIItems.Count > itemIndex)
                {
                    listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
                }
            }

            private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
            {
                int index = listOfUIItems.IndexOf(inventoryItemUI);
                if (index == -1)
                {
                    return;
                }
                OnItemActionRequested?.Invoke(index);
            }

            private void HandleEndDrag(UIInventoryItem inventoryItemUI)
            {
                ResetDraggedItem();
            }

            private void HandleSwap(UIInventoryItem inventoryItemUI)
            {
                int index = listOfUIItems.IndexOf(inventoryItemUI);
                if (index == -1)
                {
                    return;
                }
                OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
                HandleItemSelection(inventoryItemUI);
            }

            private void ResetDraggedItem()
            {
                
                currentlyDraggedItemIndex = -1;
            }

            private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
            {
                int index = listOfUIItems.IndexOf(inventoryItemUI);
                if (index == -1)
                    return;
                currentlyDraggedItemIndex = index;
                HandleItemSelection(inventoryItemUI);
                OnStartDragging?.Invoke(index);
            }

            public void CreateDraggedItem(Sprite sprite, int quantity)
            {
                
            }

            private void HandleItemSelection(UIInventoryItem inventoryItemUI)
            {
                int index = listOfUIItems.IndexOf(inventoryItemUI);
                if (index == -1)
                    return;
                OnDescriptionRequested?.Invoke(index);
            }

            public void Show()
            {
                Tab.SetActive(true);
                ResetSelection();
            }

            public void ResetSelection()
            {
                itemDescription.ResetDescription();
                DeselectAllItems();
            }

            public void AddAction(string actionName, Action performAction)
            {
                
            }

            public void ShowItemAction(int itemIndex)
            {
                
            }

            private void DeselectAllItems()
            {
                foreach (UIInventoryItem item in listOfUIItems)
                {
                    item.Deselect();
                }
                
            }

            public void Hide()
            {
                
                Tab.SetActive(false);
                ResetDraggedItem();
            }
        }
    }
