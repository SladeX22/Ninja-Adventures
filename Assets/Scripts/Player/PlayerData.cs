using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    PlayerAnimation playerAnimation;
    PlayerMana playerMana;
    PlayerHealth playerHealth;

    public PlayerStats PlayerStats => playerStats;
    //public PlayerHealth PlayerHealth=> playerHealth;

    public PlayerHealth PlayerHealth
    {
        get { return playerHealth; }
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
            if (playerStats.CurrentHealth <= 0)
                PlayerReset();
        }
    }

    public void PlayerReset()
    {
        playerStats.ResetPlayer();
        playerAnimation.HandleReviveAnimation();
        playerMana.ResetMana();
    }
}
