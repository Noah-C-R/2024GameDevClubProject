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
        Vector3 direction = enemy.transform.position - playerPos + new Vector3(0,1,0); // Offset player height
        Quaternion rotation = Quaternion.LookRotation(direction);
        //GameObject.Instantiate(projectile, spawnPos.position, spawnPos.transform.rotation);
        GameObject.Instantiate(projectile, spawnPos.position, rotation);
        isAttacking = true;
        canAttack = false;
        enemy.StartCoroutine(AttackDuration(enemy.attackTime, enemy));
    }


}
