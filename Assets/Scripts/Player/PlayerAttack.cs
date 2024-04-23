using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Weapon initialWeapon;
    [SerializeField] Transform[] attackPositions;
    
    Transform currentAttackPosition;
    float currentAttackRotation;

    PlayerMovement playerMovement;
    PlayerMana playerMana;
    PlayerActions actions;
    PlayerAnimation playerAnim;
    EnemyBrain target;
    Coroutine attackCoroutine;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnim = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMana = GetComponent<PlayerMana>();
    }

    private void Start()
    {
        actions.Attack.ClickAttack.performed += ctx => Attack();
        SelectionManager.OnEnemySelected += SetCurrentTarget;
        SelectionManager.OnNoSelection += ResetCurrentTarget;
        EnemyHealth.OnEnemyDead += KilledEnemy;
    }

    private void Update()
    {
        GetFirePosition();
    }

    private void KilledEnemy()
    {
        target = null;
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
        print(playerMana.CurrentMana);
        if(currentAttackPosition == null || playerMana.CurrentMana < initialWeapon.RequiredMana)
            yield break;

        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(initialWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up;
        projectile.Damage = initialWeapon.Damage;
        playerMana.UseMana(initialWeapon.RequiredMana);


        playerAnim.setAttackingAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnim.setAttackingAnimation(false);
    }

    void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;

        if (moveDirection.x > 0f)
        {
            currentAttackPosition = attackPositions[1]; //right
            currentAttackRotation = -90f;
        }
        else if (moveDirection.x < 0f)
        {
            currentAttackPosition = attackPositions[3]; //left
            currentAttackRotation = -270f;
        }

        if (moveDirection.y > 0f)
        {
            currentAttackPosition = attackPositions[0]; //up
            currentAttackRotation = 0f;
        }
        else if (moveDirection.y < 0f)
        {
            currentAttackPosition = attackPositions[2]; //down
            currentAttackRotation = -180f;
        }
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
