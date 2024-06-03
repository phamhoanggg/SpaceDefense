using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    protected float dmg;
    protected float speed;
    protected Transform target;
    protected Vector3 direct;
    protected GameObject attacker;

    protected bool isFlying;
    public override void OnInit()
    {
        isFlying = false;
    } 

    public virtual void AssignValues(float damage, float fly_speed, Transform target, GameObject attacker, GameLayer layer)
    {
        dmg = damage;
        speed = fly_speed;
        this.target = target;
        this.attacker = attacker;

        direct = target.position - TF.position;
        isFlying = true;
        StartCoroutine(IE_Despawn(5));
        gameObject.layer = (int)layer;
    }

    protected virtual void Update()
    {
        if (isFlying)
        {
            if (target)
            {
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
    public override void OnDespawn()
    {
        isFlying = false;
        StopAllCoroutines();
        ParticlePoolController.Instance.Play(ParticleType.Missile_Hit, TF.position);
        AudioManager.Instance.PlaySound(SoundId.Bullet_hit);
        SimplePool.Despawn(this);
    }

    public IEnumerator IE_Despawn(float despawnAfter)
    {
        yield return new WaitForSeconds(despawnAfter);
        OnDespawn();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attacker) return;

        if (collision.GetComponent<Construction>() != null && !collision.GetComponent<Construction>().isPlaced) return;

        if (collision.GetComponent<IOnLand>() != null && poolType == PoolType.Bullet_Land)
        {
            collision.GetComponent<IOnLand>().TakeLandDamage(dmg);
            OnDespawn();
        }
        else if (collision.GetComponent<IFlyable>() != null && poolType == PoolType.Bullet_Air)
        {
            collision.GetComponent<IFlyable>().TakeAirDamage(dmg);
            OnDespawn();
        }
    }
}
