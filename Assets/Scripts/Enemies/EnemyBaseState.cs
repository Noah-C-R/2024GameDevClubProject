using System;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
    public abstract void ExitState(EnemyStateManager enemy);
    protected bool IsInRangeOfPlayer(EnemyStateManager enemy)
    {
        return Math.Abs(enemy.transform.position.x-enemy.player.transform.position.x) < enemy.attackRange;
    }
    protected void RotateTowardsPlayer(EnemyStateManager enemy)
    {
        if(enemy.transform.position.x > enemy.player.transform.position.x)
        {
            enemy.transform.eulerAngles = new Vector3(0,0,0);
        }
        else
        {
            enemy.transform.eulerAngles = new Vector3(0,180,0);
        }
    }

}
