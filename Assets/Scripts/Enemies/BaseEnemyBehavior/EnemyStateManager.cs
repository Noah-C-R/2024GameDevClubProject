using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    protected EnemyBaseState currentState;
    public GameObject player;
    public float speed = 10.0f;
    public float attackRange = 5.0f;
    public float attackCooldown = 1.0f;
    public float attackTime = 300.0f; // Temporary variable mean to represent attack time
    public Dictionary<string,EnemyBaseState> enemyStates;


    private void Start()
    {
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
