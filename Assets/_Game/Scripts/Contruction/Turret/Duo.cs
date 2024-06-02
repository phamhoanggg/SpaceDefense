using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duo : Turret, ILandAttackable
{
    public override void Attack(float dmg, Transform target)
    {
        AttackOnLand(dmg, target);
        Debug.Log("Duo attack");
    }
    public void AttackOnLand(float dmg, Transform target)
    {
        Debug.Log("Duo attack on land");
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Land, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Player_Bullet);
    }
}
