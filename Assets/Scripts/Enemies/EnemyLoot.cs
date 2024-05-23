using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] float xPDropped;
    [SerializeField] DroppedItem[] lootTable;
    public List<DroppedItem> DroppedItems { get; private set; }
    public float XPDropped { get { return xPDropped; } }

    private void Start()
    {
        LoadDropItems();
    }

    public void LoadDropItems()
    {
        DroppedItems = new List<DroppedItem>();

        foreach(var item in lootTable)
        {
            float prob = Random.Range(0f, 100f);
            if(prob <= item.DropChance)
                DroppedItems.Add(item);
        }
    }
}

[System.Serializable]
public class DroppedItem
{
    public string Name;
    public InventoryItem Item;
    public int Quantity;
    public float DropChance;

    public bool PickedItem { get; set; }


}