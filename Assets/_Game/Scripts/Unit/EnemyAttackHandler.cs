using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] protected float dmg;
    [SerializeField] protected float atk_range;
    [SerializeField] protected float atk_speed;
    [SerializeField] protected CircleCollider2D circleCol;
    [SerializeField] protected GameObject centerModule;
    [SerializeField] protected Enemy parent;
    private List<GameObject> targetList;
    private GameObject curTarget;
    private float atk_CD;

    private void Start()
    {
        targetList = new List<GameObject>();
        circleCol.radius = atk_range;
        centerModule = FindObjectOfType<CenterModule>().gameObject;
    }
    // Update is called once per frame
    protected void Update()
    {
        if (parent.IsDead) return;

        if (atk_CD > 0)
        {
            atk_CD -= Time.deltaTime;
        }
        else
        {
            if (targetList.Count == 0) return;

            if (targetList.Contains(centerModule) && curTarget != centerModule)
            {
                curTarget = centerModule;
            }

            if (curTarget)
            {
                Attack(curTarget.transform);
                atk_CD = 1 / atk_speed;
            }
            else
            {
                curTarget = GetTarget();
            }
        }
    }
    public virtual void Attack(Transform target)
    {
        if (target.GetComponent<IFlyable>() != null && GetComponent<IAirAttackable>() != null)
        {
            AttackOnAir(dmg, target);
        }
        else if (target.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            AttackOnLand(dmg, target);
        }
    }

    public virtual void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, transform.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Enemy);
    }

    public virtual void AttackOnLand(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Land, transform.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Enemy);
    }

    public GameObject GetTarget()
    {
        if (parent.IsDead) return null;
        int minIndex = 0;
        float minDis = 100000;
        for (int i = 0; i < targetList.Count; i++) {
            if (targetList[i] == null)
            {
                targetList.Remove(targetList[i]);
                continue;
            }
        }

        for (int i = 0; i < targetList.Count; i++)
        {
            if (Vector2.Distance(transform.position, targetList[i].transform.position) < minDis)
            {
                minDis = Vector2.Distance(transform.position, targetList[i].transform.position);
                minIndex = i;
            }
        }

        if (minIndex < 0 || minIndex >= targetList.Count)
        {
            return null;
        }
        else
        {
            return targetList[minIndex];
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            targetList.Add(collision.gameObject);
        }

        if (collision.GetComponent<IFlyable>() != null && GetComponent<IAirAttackable>() != null)
        {
            targetList.Add(collision.gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (targetList.Contains(collision.gameObject))
        {
            targetList.Remove(collision.gameObject);
            if (curTarget == collision.gameObject)
            {
                curTarget = GetTarget();
            }
        }
    }
}
