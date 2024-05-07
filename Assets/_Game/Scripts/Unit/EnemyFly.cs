using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : Enemy, IFlyable
{
    public override void TakeAirDamage(float dmg)
    {
        if (GameManager.Instance.gameConfig.isOneHit)
        {
            OnDead();
            return;
        }
        cur_HP -= dmg;
        if (cur_HP <= 0)
        {
            OnDead();
        }
    }
}
