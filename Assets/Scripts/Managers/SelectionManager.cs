using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    private Camera mainCamera;
    public static event Action<EnemyBrain> OnEnemySelected;
    public static event Action OnNoSelection;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, enemyMask);
            if(hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                if(enemy != null)
                {
                    if(enemy.TryGetComponent(out EnemyHealth enemyHealth) && enemy.GetComponent<EnemyHealth>().CurrentHealth <= 0)
                    {
                        EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                        LootManager.i.ShowLoot(enemyLoot);
                        LootManager.i.CheckHasItemsLeft(enemy.gameObject);
                    }
                    else
                    {
                        OnEnemySelected?.Invoke(enemy);
                    }
                }
            }
            else
            {
                OnNoSelection?.Invoke();
            }
        }
    }
}
