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

    public override void Moving()
    {
        if (centerModule == null) return;
        if (Vector2.Distance(tf.position, centerModule.position) > atk_range)
        {
            tf.position = Vector2.MoveTowards(tf.position, centerModule.position, move_speed * Time.deltaTime);
            Vector2 direct = centerModule.position - tf.position;
            float angleZ = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            tf.eulerAngles = new Vector3(0, 0, angleZ - 90);
        }
    }
}
