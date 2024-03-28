using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerActions actions;
    PlayerAnimation playerAnim;
    EnemyBrain target;
    Coroutine attackCoroutine;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnim = GetComponent<PlayerAnimation>();
        
    }

    private void Start()
    {
        actions.Attack.ClickAttack.performed += ctx => Attack();
        SelectionManager.OnEnemySelected += SetCurrentTarget;
        SelectionManager.OnNoSelection += ResetCurrentTarget;
    }

    void Attack()
    {
        if(target == null)
            return;
        if(attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        print("Attacking");
        playerAnim.setAttackingAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnim.setAttackingAnimation(false);
    }



    void SetCurrentTarget(EnemyBrain selectedTarget)
    {
        target = selectedTarget;
    }

    void ResetCurrentTarget()
    {
        target = null;
    }





    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
