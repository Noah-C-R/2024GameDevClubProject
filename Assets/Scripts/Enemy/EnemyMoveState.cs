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
        enemy.transform.position += new Vector3(direction.x,0,0) * enemy.enemyStats.speed * Time.deltaTime;
        if(isInRangeOfPlayer(enemy))
        {
            enemy.SwitchState("Attack");
        }
    }

    private bool isInRangeOfPlayer(EnemyStateManager enemy)
    {
        return Math.Abs(enemy.transform.position.x-enemy.player.transform.position.x) < enemy.enemyStats.attackRange;
    } 
    public override void ExitState(EnemyStateManager enemy)
    {

    }
}
