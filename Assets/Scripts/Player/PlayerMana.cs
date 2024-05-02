using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public float CurrentMana { get; private set; }

    private void Start()
    {
        ResetMana();
    }

    public bool CanRestoreMana()
    {
        return playerStats.CurrentMana > 0f && playerStats.CurrentMana < playerStats.MaxMana;
    }
    public void RestoreMana(float amount)
    {
        playerStats.CurrentMana += amount;
        playerStats.CurrentMana = Mathf.Min(playerStats.CurrentMana, playerStats.MaxMana);
    }

    public void ResetMana()
    {
        CurrentMana = playerStats.MaxMana;
    }

    public void UseMana(float amount)
    {
        playerStats.CurrentMana = Mathf.Max(playerStats.CurrentMana -= amount, 0f);
        CurrentMana = playerStats.CurrentMana;
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            useMana(5);
        }
    }*/
}
