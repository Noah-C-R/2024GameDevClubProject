using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
     Debug.Log("Enter Move State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Vector3 direction = new Vector3(1,0,0);
        if(enemy.transform.position.x > enemy.player.transform.position.x)
        {
            direction *=-1;
        }
        RotateTowardsPlayer(enemy);
        enemy.transform.position += direction * enemy.enemyStats.speed * Time.deltaTime;

        if(IsInRangeOfPlayer(enemy))
        {
            enemy.SwitchState("Attack");
        }
    }

    
    public override void ExitState(EnemyStateManager enemy)
    {

    }
}
