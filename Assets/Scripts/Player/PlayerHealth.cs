using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] PlayerStats playerStats;

    PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playerStats.CurrentHealth > 0)
                TakeDamage(1f);
        }
    }

    public void TakeDamage(float amount)
    {
        playerStats.CurrentHealth -= amount;
        

        if (playerStats.CurrentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        playerAnimation.HandleDeadAnimation();
    }
}
