using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    [NonSerialized]public EnemyStats enemyStats;
    public GameObject player;
    
    public Dictionary<string,EnemyBaseState> enemyStates;


    void Start()
    {
        enemyStats = gameObject.GetComponent<EnemyStats>();

        InitializeStates();
        currentState = enemyStates["Idle"];
        currentState.EnterState(this);
    }
    public virtual void InitializeStates()
    {
        enemyStates = new Dictionary<string, EnemyBaseState>
        {
            {"Idle", new EnemyIdleState()},
            {"Attack", new EnemyAttackState()},
            {"Move", new EnemyMoveState()}

        };
    }

    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(string newState)
    {
        currentState.ExitState(this);
        currentState = enemyStates[newState];
        currentState.EnterState(this);
    }
}
