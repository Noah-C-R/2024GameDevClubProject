public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(AttackEnd() && !IsInRangeOfPlayer(enemy))
        {
            enemy.SwitchState("Move");
        }
    }
    public override void ExitState(EnemyStateManager enemy)
    {

    }
    private bool AttackEnd()
    {
        return true;
    }
}
