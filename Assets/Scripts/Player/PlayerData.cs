using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
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
    }
}
