using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter : Turret, IAirAttackable
{
    public override void Attack(float dmg, Transform target)
    {
        AttackOnAir(dmg, target);
    }
    public void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Player_Bullet);
    }

}
