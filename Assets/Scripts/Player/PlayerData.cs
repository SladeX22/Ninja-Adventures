using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Item_ManaPotion manaPotion;

    PlayerAnimation playerAnimation;
    PlayerMana playerMana;
    PlayerHealth playerHealth;

    public PlayerStats PlayerStats => playerStats;
    //public PlayerHealth PlayerHealth=> playerHealth;

    public PlayerHealth PlayerHealth
    {
        get { return playerHealth; }
    }

    public PlayerMana PlayerMana
    {
        get { return playerMana; }
    }

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMana = GetComponent<PlayerMana>();
        playerHealth = GetComponent<PlayerHealth>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var result = manaPotion.UseItem();
            if (result)
            {
                print("Used mana potion");
            }
        }
    }

    public void PlayerReset()
    {
        playerStats.ResetPlayer();
        playerAnimation.HandleReviveAnimation();
        playerMana.ResetMana();
    }
}
