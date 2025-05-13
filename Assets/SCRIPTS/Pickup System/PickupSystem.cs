using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{

    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private InventorySO inventoryData1;
    [SerializeField]
    private InventorySO inventoryData2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
        if (collision.gameObject.CompareTag("Fragment"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                int reminder = inventoryData1.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }
        if(collision.gameObject.CompareTag("Poem"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                int reminder = inventoryData2.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                    item.DestroyItem();
                else
                    item.Quantity = reminder;
            }
        }


    }

}
