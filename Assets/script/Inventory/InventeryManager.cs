using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yushan.Enums;

public class InventeryManager : SingletonMonobehaviour<InventeryManager>
{
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    public List<InventoryItem>[] inventoryLists;

    [HideInInspector] public int[] inventoryListCapacityIntArray;// index of the array is the inventory list from the inventorylocation enum and the value is the capacity of that inventory list

    [SerializeField]
    private SO_ItemList itemList = null;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        base.Awake();

        CreateInventoryLists();

        CreateItemDetailsDictionary();
    }

    private void CreateInventoryLists()
    {
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];
        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();

        }
        // initialise inventory list capacity array
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];



        // initialise player inventory list capacity
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;

    }
    /// <summary>
    /// add an item to the inventory list for the inventorylocation and then destroy the gameobjectodelete
    /// </summary>
    /// <param name="inventoryList"></param>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// add an item to the inventory list for the inventoryLocation
    /// </summary>

    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // check if inventory already contains the item
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation]
;
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                Debug.Log("if(inventoryList[i].itemCode == itemCode) return i" + i);
                return i;
            }
        }
        return -1;
    }







    /// <summary>
    /// add item to inventory
    /// </summary>
    /// <param name="inventoryList"></param>
    /// <param name="itemCode"></param>
    /// <param name="position"></param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;

        DebugPrintInventoryList(inventoryList);
    }
    /// <summary>
    /// add item to the end of the inventory
    /// </summary>
    /// <param name="inventoryList"></param>
    /// <param name="itemCode"></param>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        DebugPrintInventoryList(inventoryList);
    }
    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemdetails;

        if (itemDetailsDictionary.TryGetValue(itemCode, out itemdetails))
        {
            Debug.Log("getitemdetails itemcode" + itemCode);
            return itemdetails;
        }
        else
        {
            return null;
        }
    }
    private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log("item description" + InventeryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription + "item quautity" + inventoryItem.itemQuantity);
        }
        Debug.Log("****************");
    }
}
