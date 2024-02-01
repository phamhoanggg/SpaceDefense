using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float dmg;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    private Vector3 direct;
    private GameObject attacker;

    private bool isFlying;
    public override void OnInit()
    {
        isFlying = false;
    } 

    public void AssignValues(float damage, float fly_speed, Transform target, GameObject attacker, GameLayer layer)
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

    private void Update()
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
        SimplePool.Despawn(this);
    }

    public IEnumerator IE_Despawn(float despawnAfter)
    {
        yield return new WaitForSeconds(despawnAfter);
        OnDespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attacker) return;

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
