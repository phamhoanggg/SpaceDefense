using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : Enemy, IFlyable
{
    public void TakeAirDamage(float dmg)
    {
        if (GameManager.Instance.gameConfig.isOneHit)
        {
            OnDead();
            return;
        }

        cur_HP -= dmg;
        Debug.Log("Take DMG");
        if (cur_HP <= 0)
        {
            OnDead();
        }
    }
}
