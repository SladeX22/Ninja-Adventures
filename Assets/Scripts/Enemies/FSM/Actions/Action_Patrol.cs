using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Patrol : FSM_Action
{
    [SerializeField] float patrolSpeed;

    int currentPoint;
    Waypoint waypoint;
    EnemyAnimation enemyAnimation;

    private void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        waypoint = GetComponent<Waypoint>();
    }

    public override void Act()
    {
        FollowPatrolPath();
    }

    void FollowPatrolPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint.GetPosition(currentPoint), patrolSpeed * Time.deltaTime);


        if(Vector3.Distance(transform.position, waypoint.GetPosition(currentPoint)) <= 0.1f)
        {
            currentPoint = (currentPoint + 1) % waypoint.Points.Length;
        }

        Vector2 dir = (waypoint.GetPosition(currentPoint) - transform.position).normalized;
        Vector2 movement = dir * (patrolSpeed * Time.deltaTime);

        enemyAnimation.HandleMovingAnimation(movement);
    }
}
