using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    protected override void Update()
    {
        if (isFlying)
        {
            if (target)
            {
                direct = target.position - TF.position;
                TF.position += speed * Time.deltaTime * direct.normalized;
                float angleZ = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
                TF.eulerAngles = new Vector3(0, 0, angleZ - 90);
            }
            else
            {
                OnDespawn();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attacker) return;

        if (collision.GetComponent<IFlyable>() != null)
        {
            collision.GetComponent<IFlyable>().TakeAirDamage(dmg);
            OnDespawn();
        }
    }

    public override void OnDespawn()
    {
        ParticlePoolController.Instance.Play(ParticleType.Missile_Hit, TF.position);
        base.OnDespawn();
    }
}
