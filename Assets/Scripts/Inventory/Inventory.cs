using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] int inventorySize;
    [SerializeField] InventoryItem[] inventoryItems;

    public InventoryItem testItem;
    [SerializeField] GameContent gameContent;

    public int InventorySize => inventorySize;
    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        //CheckSlotForItem();
        LoadInventory();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 2);
        }
    }

    public void EquipItem(int index)
    {
        if(inventoryItems[index] == null)
            return;
        if(inventoryItems[index].Type != ItemType.WEAPON)
            return;

        inventoryItems[index].EquipItem();
        
    }

    public void UseItem(int index)
    {
        if(inventoryItems[index] == null)
            return;
        if(inventoryItems[index].UseItem())
            DecreaseItem(index);

        SaveIntentory();
    }

    void DecreaseItem(int index)
    {
        inventoryItems[index].Quantity--;

        if(inventoryItems[index].Quantity <= 0 )
        {
            inventoryItems[index] = null;
            InventoryUI.i.DrawItem(null, index);
        }
        else
        {
            InventoryUI.i.DrawItem(inventoryItems[index], index);
        }
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        if(item == null || quantity <= 0)
            return;

        List<int> itemIndexes = CheckItemStock(item.ID);

        if(item.IsStackable && itemIndexes.Count > 0)
        {
            foreach(int index in itemIndexes)
            {
                int currentMaxStack = item.MaxStack;

                if(inventoryItems[index].Quantity < currentMaxStack)
                {
                    inventoryItems[index].Quantity += quantity;

                    if(inventoryItems[index].Quantity > currentMaxStack)
                    {
                        int diff = inventoryItems[index].Quantity - currentMaxStack;
                        inventoryItems[index].Quantity = currentMaxStack;
                        AddItem(item, diff);
                    }

                    InventoryUI.i.DrawItem(inventoryItems[index], index);
                    SaveIntentory();
                    return;
                }

            }
        }

        int quantityToAdd = (quantity > item.MaxStack) ? item.MaxStack : quantity;
        AddItemToFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if(remainingAmount > 0)
            AddItem(item, remainingAmount);

        SaveIntentory();
    }
    
    public void RemoveItem(int index)
    {
        if(inventoryItems[index] == null)
            return;


        InventoryUI.i.descriptionPanel.SetActive(false);
        InventoryUI.i.CurrentSlot = null;
        inventoryItems[index] = null;
        InventoryUI.i.DrawItem(null, index);
        SaveIntentory();
    }


    List<int> CheckItemStock(string itemID)
    {
        List<int> itemIndexes = new List<int>();

        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if(inventoryItems[i] == null)
                continue;

            if(inventoryItems[i].ID == itemID)
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
            if(inventoryItems[i] != null)
                continue;

            inventoryItems[i] = item.CreateItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.i.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    void CheckSlotForItem()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if(inventoryItems[i] == null)
                InventoryUI.i.DrawItem(null, i);
        }
    }


    void SaveIntentory()
    {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];

        for(int i = 0; i < inventorySize; i++)
        {
            InventoryItem currItem = inventoryItems[i];
            if(currItem == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = currItem.ID;
                saveData.ItemQuantity[i] = currItem.Quantity;
            }
        }

       SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }

    InventoryItem ItemInGameContent(string itemID)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if(gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }

        return null;
    }

    void LoadInventory()
    {
        if(SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);

            for(int i = 0; i < inventorySize; i++)
            {
                if(loadData.ItemContent[i] != null)
                {
                    InventoryItem itemFromContent = ItemInGameContent(loadData.ItemContent[i]);

                    if(itemFromContent != null)
                    {
                        inventoryItems[i] = itemFromContent.CreateItem();
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.i.DrawItem(inventoryItems[i], i);
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                }
            }
        }


    }

}
