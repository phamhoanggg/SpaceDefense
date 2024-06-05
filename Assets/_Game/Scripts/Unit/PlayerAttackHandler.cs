using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour, IAirAttackable, ILandAttackable
{
    [SerializeField] protected float dmg;
    [SerializeField] protected float atk_range;
    [SerializeField] protected float atk_speed;
    [SerializeField] protected CircleCollider2D circleCol;
    [SerializeField] Transform TF;

    private List<Enemy> enemyList;
    private float atk_CD;
    private Enemy target;

    void Start()
    {
        enemyList = new List<Enemy>();
        circleCol.radius = atk_range;
    }

    void Update()
    {
        if (atk_CD > 0)
        {
            atk_CD -= Time.deltaTime;
        }
        else
        {
            if (target != null)
            {
                if (Vector2.Distance(TF.position, target.TF.position) > atk_range + 1f)
                {
                    enemyList.Remove(target);
                    target = null;
                }
                else
                {
                    Attack(dmg, target.TF);
                    atk_CD = 1 / atk_speed;
                }
            }
            else
            {
                target = GetTarget();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>() != null && !enemyList.Contains(collision.GetComponent<Enemy>()))
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && enemyList.Contains(collision.GetComponent<Enemy>()))
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
        }
    }

    public Enemy GetTarget()
    {
        float minDis = atk_range + 0.5f;
        int nearestEnemyIndex = -1;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null) {
                enemyList.RemoveAt(i);
                continue;
            }
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

    public void Attack(float dmg, Transform target)
    {
        AudioManager.Instance.PlaySound(SoundId.Bullet_fire);
        if (target.GetComponent<IFlyable>() != null)
        {
            AttackOnAir(dmg, target);
        }
        else if (target.GetComponent<IOnLand>() != null)
        {
            AttackOnLand(dmg, target);
        }
    }

    public void AttackOnAir(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Air, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, TF.gameObject, GameLayer.Player_Bullet);
    }

    public void AttackOnLand(float dmg, Transform target)
    {
        Bullet newBullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_Land, TF.position, Quaternion.identity);
        newBullet.AssignValues(dmg, 20, target, TF.gameObject, GameLayer.Player_Bullet);
    }
}
