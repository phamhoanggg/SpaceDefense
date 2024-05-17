using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLand : Enemy, IOnLand
{
    public void TakeLandDamage(float dmg)
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
