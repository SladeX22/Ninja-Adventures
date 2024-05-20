using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Item_Weapon")]
public class Item_Weapon : InventoryItem
{
    public Weapon Weapon;

    public override bool EquipItem()
    {
        WeaponManager.i.EquipWeapon(Weapon);
        return true;
    }

}
