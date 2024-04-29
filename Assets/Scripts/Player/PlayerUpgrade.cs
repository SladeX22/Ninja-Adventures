using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgrade;

    [SerializeField] PlayerStats playerStats;
    [SerializeField] UpgradeValues[] settings;

    void UpgradePlayer(int index)
    {
        playerStats.BaseDamage += settings[index].DamageIncrease;
        playerStats.TotalDamage += settings[index].DamageIncrease;
        playerStats.MaxHealth += settings[index].HealthIncrease;
        playerStats.CurrentHealth += playerStats.MaxHealth;
        playerStats.MaxMana += settings[index].ManaIncrease;
        playerStats.CurrentMana += playerStats.MaxMana;
        playerStats.CriticalChance += settings[index].CChanceIncrease;
        playerStats.CriticalDamage += settings[index].CDamageIncrease;
    }
}


[System.Serializable]
public class UpgradeValues
{
    public string name;
    public float DamageIncrease;
    public float HealthIncrease;
    public float ManaIncrease;
    public float CChanceIncrease;
    public float CDamageIncrease;
}
