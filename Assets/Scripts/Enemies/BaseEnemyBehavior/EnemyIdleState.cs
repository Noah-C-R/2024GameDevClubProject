using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Enter Idle State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.SwitchState("Move");
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Leave Idle State");
    }

}
