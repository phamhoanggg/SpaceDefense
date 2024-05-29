using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLand_AttackHandler : EnemyAttackHandler, ILandAttackable
{
    public override void Attack(Transform target)
    {
        if (target.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            AttackOnLand(dmg, target);
        }
    }
    public void AttackOnLand(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Land, transform.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Enemy);
    }
}
