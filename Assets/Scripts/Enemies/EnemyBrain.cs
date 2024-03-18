using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    NONE, WANDER, PATROL, CHASE, ATTACK
}

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] EnemyState initState;
    [SerializeField] FSM_State[] states;
    public FSM_State CurrentState { get; set; }
    public Transform Player { get; set; }


    void Start()
    {
        ChangeState(initState);
    }

    private void Update()
    {
        if(CurrentState == null)
            return;

        CurrentState.UpdateState(this);
    }

    public void ChangeState(EnemyState newStateID)
    {
        FSM_State newState = GetState(newStateID);

        if(newState == null)
            return;

        CurrentState = newState;
    }


    FSM_State GetState(EnemyState newStateID)
    {
        for(int i = 0; i < states.Length; i++)
        {
            if(states[i].ID == newStateID)
            {
                return states[i];
            }
        }

        return null;
    }
}