using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    PlayerAnimation playerAnimation;
    PlayerMana playerMana;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMana = GetComponent<PlayerMana>();
    }

    public PlayerStats PlayerStats => playerStats;

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
