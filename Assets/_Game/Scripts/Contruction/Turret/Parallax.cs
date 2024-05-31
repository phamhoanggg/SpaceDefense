using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : Turret, IAirAttackable
{
    public override void Attack(float dmg, Transform target)
    {
        Debug.Log("Parallax Attak");

        AttackOnAir(dmg, target);
    }
    public void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Missile, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 10, target, gameObject, GameLayer.Construction);
    }

}
