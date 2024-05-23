using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform container;
    
    public GameObject descriptionPanel;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    List<InventorySlot> slotList = new List<InventorySlot>();

    public InventorySlot CurrentSlot { get; set; }

    public override void Awake()
    {
        base.Awake();
        InitInventory();
    }

    private void Start()
    {
        InventorySlot.OnSlotSelected += SlotSelected;
    }

    void InitInventory()
    {
        for(int i = 0; i < Inventory.i.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    public void ShowItemDescription(int index)
    {
        if(Inventory.i.InventoryItems[index] == null)
            return;

        descriptionPanel.SetActive(true);
        itemIcon.sprite = Inventory.i.InventoryItems[index].Icon;
        itemName.text = Inventory.i.InventoryItems[index].Name;
        itemDescription.text = Inventory.i.InventoryItems[index].Description;
    }

    void SlotSelected(int index)
    {
        CurrentSlot = slotList[index];
        ShowItemDescription(index);
    }

    public void EquipItem()
    {
        if(CurrentSlot == null)
            return;

        Inventory.i.EquipItem(CurrentSlot.Index);
    }

    public void UseItem()
    {
        if(CurrentSlot != null)
            Inventory.i.UseItem(CurrentSlot.Index);
    }

    public void RemoveItem()
    {
        if(CurrentSlot == null)
            return;

        Inventory.i.RemoveItem(CurrentSlot.Index);
    }


    public void DrawItem(InventoryItem item, int index)
    {
        print(slotList.Count);
        print("Index: " + index);
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
