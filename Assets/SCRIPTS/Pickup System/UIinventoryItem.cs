using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Inventory.UI
{

    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private GameObject borderImage;

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void Deselect()
        {
            
        }
        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            
        }

        public void OnPointerClick(BaseEventData data)
        {   PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }

}

