using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] float health;
    Animator anim;
    public float CurrentHealth { get; private set; }
    public static event Action OnEnemyDead;
    EnemyLoot enemyLoot;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0f)
        {
            DisableEnemy();
        }
        else
        {
            DamageManager.i.ShowDamageText(amount, transform);
        }
    }

    private void DisableEnemy()
    {
        anim.SetTrigger("gotKilled");
        GetComponent<EnemyBrain>().enabled = false;
        GetComponent<EnemySelector>().DeactivateSelector();
        OnEnemyDead?.Invoke();
        GameManager.i.AddPlayerXP(enemyLoot.XPDropped);
    }
}
