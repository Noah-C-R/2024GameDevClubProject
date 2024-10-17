using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackState : EnemyBaseState
{
    protected bool canAttack = true;
    protected bool isAttacking = false;
    protected private bool cooldownRunning = false;
    
    public override void EnterState(EnemyStateManager enemy)
    {
        isAttacking = false;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
        // If not attacking and not in range of player switch to move state
        // If not attacking, in range of player and not on attack cooldown then attack
        if(!isAttacking)
        {
            RotateTowardsPlayer(enemy);
            if(!IsInRangeOfPlayer(enemy))
            {
                enemy.SwitchState("Move");
                return;
            }
            if(canAttack)
            {
                Attack(enemy);
            }
            else
            {
                //Debug.Log("Cooling down");
            }
            
        }
        else
        {
            //Debug.Log("Attacking!");
        }
       
    }
    public override void ExitState(EnemyStateManager enemy)
    {
       
    }
    protected virtual void OnAttackEnd(EnemyStateManager enemy)
    {
        isAttacking = false;
        enemy.StartCoroutine(AttackCooldown(enemy.attackCooldown));
        if(!IsInRangeOfPlayer(enemy))
        {
            enemy.SwitchState("Move");
            return;
        }

    }
    protected virtual bool AttackEnd(EnemyStateManager enemy)
    {
        // Check if animation is done
       
       return true;
    }
    protected virtual void Attack(EnemyStateManager enemy)
    {
        isAttacking = true;
        canAttack = false;
        enemy.StartCoroutine(AttackDuration(enemy.attackTime, enemy));
    }
    public IEnumerator AttackDuration(float length, EnemyStateManager enemy)
    {
        yield return new WaitForSeconds(length);
        isAttacking = false;
        enemy.StartCoroutine(AttackCooldown(enemy.attackCooldown));
    }
    public IEnumerator AttackCooldown(float cooldown)
    {
        cooldownRunning = true;
        yield return new WaitForSeconds(cooldown);
        cooldownRunning = false;
        canAttack = true;

    }
}
