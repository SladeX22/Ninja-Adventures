using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Edible", menuName = "Items/Edible")]
public class Item_Edible : InventoryItem
{
    public float HealthRestored;
    public float ManaRestored;

    public override bool UseItem()
    {
        bool isFullHealth = GameManager.i.PlayerData.PlayerHealth.CanRestoreHealth();
        bool isFullMana = GameManager.i.PlayerData.PlayerMana.CanRestoreMana();

        if(isFullHealth && isFullMana)
            return false;
        
        if(!isFullHealth)
            GameManager.i.PlayerData.PlayerHealth.RestoreHealth(HealthRestored);
        
        if(!isFullMana)
            GameManager.i.PlayerData.PlayerMana.RestoreMana(ManaRestored);

        return true;
    }
}
