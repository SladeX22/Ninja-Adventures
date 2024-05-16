using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform container;
    
    List<InventorySlot> slotList = new List<InventorySlot>();

    public InventorySlot CurrentSlot { get; set; }
    

    private void Start()
    {
       InitInventory();
        InventorySlot.OnSlotSelected += SlotSelected;

    }

    void InitInventory()
    {
        for (int i = 0; i < Inventory.i.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    void SlotSelected(int index)
    {
        CurrentSlot = slotList[index];
    }

    public void RemoveItem()
    {
        if(CurrentSlot == null)
            return;

        Inventory.i.RemoveItem(CurrentSlot.Index);
    }


    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if(item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }

        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
}
