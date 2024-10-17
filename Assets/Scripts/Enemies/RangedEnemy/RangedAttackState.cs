using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : EnemyAttackState
{
    private GameObject projectile;
    private Transform spawnPos;

    public RangedAttackState(GameObject projectile, Transform spawnPos)
    {
        this.projectile = projectile;
        this.spawnPos = spawnPos;
    }
    
    protected override void Attack(EnemyStateManager enemy)
    {
        Vector3 playerPos = enemy.player.transform.position;
        GameObject.Instantiate(projectile, spawnPos.position, spawnPos.transform.rotation);
        isAttacking = true;
        canAttack = false;
        enemy.StartCoroutine(AttackDuration(enemy.attackTime, enemy));
    }


}
