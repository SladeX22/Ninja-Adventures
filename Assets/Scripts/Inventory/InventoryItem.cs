using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON, POTION, SCROLL, INGREDIENTS, TREASURE
}

[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    public ItemType Type;
    public bool IsConsumable;
    public bool IsStackable;
    public int MaxStack;
    [HideInInspector] public int Quantity;

    public InventoryItem CreateItem()
    {
        InventoryItem item = Instantiate(this);
        return item;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool EquipItem()
    {
        return true;
    }

    public virtual bool RemoveItem()
    {
        return true;
    }
}
