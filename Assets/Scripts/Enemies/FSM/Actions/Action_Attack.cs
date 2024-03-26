using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Action_Attack : FSM_Action
{
    [SerializeField] float damage;
    [SerializeField] float timeBetweenAtacks;

    EnemyBrain brain;
    float attackTimer;

    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
    }

    private void Start()
    {
        attackTimer = timeBetweenAtacks;
    }

    public override void Act()
    {
        AttackPlayer();

    }

    void AttackPlayer()
    {
        if(brain.Player == null)
            return;

        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f)
        {
            IDamagable player = brain.Player.GetComponent<IDamagable>();
            player.TakeDamage(damage);
            attackTimer = timeBetweenAtacks;
        }


    }
}
