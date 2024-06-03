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
    [SerializeField] private Construction baseTurret;
    public Transform TF => tf;
    private float atk_CD = 0;
    private List<Enemy> enemyList;
    Enemy target;

    private bool isRotatingIdle;
    float targetAngle;
    protected void Start()
    {
        enemyList = new List<Enemy>();
        circleCol.radius = atk_range;
    }
    protected void Update()
    {
        if (!baseTurret.isPlaced) return;

        if (enemyList.Count > 0)
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
                    if (Vector2.Distance(TF.position, target.TF.position) > atk_range + baseTurret.Construction_Width / 2f + 0.5f)
                    {
                        enemyList.Remove(target);
                        Debug.Log(gameObject.name + " removed " + target.gameObject.name);
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
        else
        {
            if (!isRotatingIdle)
            {
                targetAngle = Random.Range(0, 360f);
                isRotatingIdle = true;
            }
            else
            {
                TF.eulerAngles += new Vector3(0, 0, targetAngle - TF.eulerAngles.z) * Time.deltaTime;
                if (Mathf.Abs(TF.eulerAngles.z - targetAngle) < 1)
                {
                    isRotatingIdle = false;
                }
            }
            
        }
    }

    public virtual void Attack(float dmg, Transform target) { }


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
            Debug.Log(gameObject.name + " added enemy");
        }

        if (collision.GetComponent<Enemy>() != null && collision.GetComponent<IOnLand>() != null && GetComponent<ILandAttackable>() != null)
        {
            enemyList.Add(collision.GetComponent<Enemy>());
            Debug.Log(gameObject.name + " added enemy");
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && enemyList.Contains(collision.GetComponent<Enemy>()))
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
            Debug.Log(gameObject.name + " removed enemy");

        }
    }
}
