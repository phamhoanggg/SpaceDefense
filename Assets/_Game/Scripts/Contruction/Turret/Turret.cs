using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected float atk_speed;
    [SerializeField] protected float dmg;
    [SerializeField] protected float atk_range;
    [SerializeField] protected CircleCollider2D circleCol;
    [SerializeField] protected Transform tf;
    [SerializeField] private Contruction baseTurret;
    public Transform TF => tf;
    private float atk_CD = 0;
    private List<Enemy> enemyList;
    Enemy target;

    protected void Start()
    {
        enemyList = new List<Enemy>();
        circleCol.radius = atk_range;
    }
    protected void Update()
    {
        if (baseTurret.isPlaced && enemyList.Count > 0)
        {
            if (atk_CD > 0)
            {
                atk_CD -= Time.deltaTime;
            }
            else
            {
                if (target != null)
                {
                    Vector2 direct = target.transform.position - TF.position;
                    float angleZ = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg - 90;
                    TF.eulerAngles = new Vector3(0, 0, angleZ);
                    if (Vector2.Distance(TF.position, target.TF.position) > atk_range)
                    {
                        enemyList.Remove(target);
                    }
                    else
                    {
                        AttackOnAir(dmg, target.TF);
                        atk_CD = 1 / atk_speed;
                    }
                }
                else
                {
                    target = GetTarget();
                }
            }
        }      
    }
    public void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Construction);
    }

    public void AttackOnLand(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, gameObject, GameLayer.Construction);
    }

    public Enemy GetTarget()
    {
        float minDis = atk_range + 0.5f;
        int nearestEnemyIndex = -1;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (Vector2.Distance(TF.position, enemyList[i].TF.position) <= minDis)
            {
                nearestEnemyIndex = i;
                minDis = Vector2.Distance(TF.position, enemyList[i].TF.position);
            }
            else
            {
                enemyList.RemoveAt(i);
            }
        }

        if (nearestEnemyIndex > -1)
        {
            return enemyList[nearestEnemyIndex];
        }
        else return null;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>() != null && collision.GetComponent<IFlyable>() != null && GetComponent<IAirAttackable>() != null)
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }

        if (collision.GetComponent<Enemy>() != null && collision.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && enemyList.Contains(collision.GetComponent<Enemy>()))
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
        }

        if (collision.GetComponent<Enemy>() != null && collision.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
        }
    }
}
