using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly_AttackHandler : EnemyAttackHandler, ILandAttackable, IAirAttackable
{
    public override void Attack(Transform target)
    {
        if (target.GetComponent<IFlyable>() != null && GetComponent<IAirAttackable>() != null)
        {
            AttackOnAir(dmg, target);
        }
        else if (target.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            AttackOnLand(dmg, target);
        }
    }
    public void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, transform.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Enemy);
    }

    public void AttackOnLand(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Land, transform.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Enemy);
    }
}
