using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : EnemyStateManager
{
    public GameObject projectile;
    public Transform spawnPos;  // Will be at tip of gun later
    private void Start()
    {
        InitializeStates();
        currentState = enemyStates["Idle"];
        currentState.EnterState(this);
    }
    public override void InitializeStates()
    {
        enemyStates = new Dictionary<string, EnemyBaseState>
        {
            {"Idle", new EnemyIdleState()},
            {"Attack", new RangedAttackState(projectile, spawnPos)},
            {"Move", new EnemyMoveState()}

        };
    }
}
