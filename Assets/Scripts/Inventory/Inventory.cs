using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] int inventorySize;
    [SerializeField] InventoryItem[] inventoryItems;
    public InventoryItem testItem;

    public int InventorySize => inventorySize;

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 15);
        }
    }

    void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity == 0)
            return;

        List<int> itemIndexes = CheckItemStock(item.ID);

        if(item.IsStackable && itemIndexes.Count > 0)
        {
            foreach(int index in itemIndexes)
            {
                int currentMaxStack = item.MaxStack;

                if (inventoryItems[index].Quantity < currentMaxStack)
                {
                    inventoryItems[index].Quantity += quantity;

                    if (inventoryItems[index].Quantity > currentMaxStack)
                    {
                        int diff = inventoryItems[index].Quantity - currentMaxStack;
                        inventoryItems[index].Quantity = currentMaxStack;
                        AddItem(item, diff);
                    }
                }

                InventoryUI.i.DrawItem(inventoryItems[index], index);
            }
        }

        int quantityToAdd = (quantity > item.MaxStack) ? item.MaxStack : quantity;
        AddItemToFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if(remainingAmount > 0)
            AddItem(item, remainingAmount);

    }

    List<int>CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();

        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].ID == null)
                continue;

            if (inventoryItems[i].ID == itemID)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    void AddItemToFreeSlot(InventoryItem item, int quantity)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null)
                continue;

            inventoryItems[i] = item.CreateItem();
            inventoryItems[i].Quantity = quantity;
        }

    }
}
