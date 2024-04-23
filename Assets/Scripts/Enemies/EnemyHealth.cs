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

    void Awake()
    {
        anim = GetComponent<Animator>();
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
            anim.SetTrigger("gotKilled");
            GetComponent<EnemyBrain>().enabled = false;
            GetComponent<EnemySelector>().DeactivateSelector();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            OnEnemyDead?.Invoke();
        }
        else
        {
            DamageManager.i.ShowDamageText(amount, transform);
        }
    }
}
