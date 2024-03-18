using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FSM_Transition
{
    public FSM_Decision Decision;
    public EnemyState TrueState;
    public EnemyState FalseState;
}
