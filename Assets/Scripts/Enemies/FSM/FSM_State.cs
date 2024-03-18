using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateID
{
    NONE, WANDER, PATROL, CHASE, ATTACK
}

[System.Serializable]
public class FSM_State : MonoBehaviour
{
    public EnemyStateID EnemyStateID;
    public FSM_Action[] Actions;
    public FSM_Transition[] Transitions;

    void ExecuteActions()
    {
        for(int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    void ExecuteTransitions()
    {
        if (Transitions == null || Transitions.Length == 0)
            return;

        for (int i = 0; i < Transitions.Length; i++)
        {
            var value = Transitions[i].Decision.Decide();

            if(value)
            {
                //Execute True State
            }
            else
            {
                //Execute False State
            }

        }
    }
}
