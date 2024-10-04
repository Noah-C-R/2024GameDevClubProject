using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    int count = 0;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter Idle State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        // Do something
        count ++;

        if(isPlayerDetected())
        {
            enemy.SwitchState("Move");
        }
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Leave Idle State");
    }

    private bool isPlayerDetected()
    {
        return count>3000;
    }
}
