using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem slashFX;
    [SerializeField] float attackRange;
    public Weapon CurrentWeapon { get; set; }

    [SerializeField] Weapon initialWeapon;
    [SerializeField] Transform[] attackPositions;
    
    Transform currentAttackPosition;
    float currentAttackRotation;

    [SerializeField] PlayerStats playerStats;
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
        /*EquipWeapon(initialWeapon);*/
        WeaponManager.i.EquipWeapon(initialWeapon);
        actions.Attack.ClickAttack.performed += ctx => Attack();
        SelectionManager.OnEnemySelected += SetCurrentTarget;
        SelectionManager.OnNoSelection += ResetCurrentTarget;
        EnemyHealth.OnEnemyDead += KilledEnemy;
    }

    private void Update()
    {
        GetFirePosition();
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        playerStats.TotalDamage = playerStats.BaseDamage + CurrentWeapon.Damage;
    }

    float GetAttackDamage()
    {
        var damage = playerStats.BaseDamage;
        damage += CurrentWeapon.Damage;

        var critChance = Random.Range(0, 100);
        if(critChance <= playerStats.CriticalChance)
            damage += damage * (playerStats.CriticalDamage / 100);

        return damage;
    }

    private void KilledEnemy()
    {
        target = null;
    }

    void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();

        var distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if(distanceToTarget <= attackRange)
            target.GetComponent<IDamagable>()?.TakeDamage(GetAttackDamage());
    }

    private void MagicAttack()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab,
                                            currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up;
        projectile.Damage = GetAttackDamage();

        playerMana.UseMana(CurrentWeapon.RequiredMana);
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
        if(currentAttackPosition == null) yield break;

        if(CurrentWeapon.WeaponType == WeaponType.MAGIC)
        {
            if(playerMana.CurrentMana < CurrentWeapon.RequiredMana)
                yield break;
            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }

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
