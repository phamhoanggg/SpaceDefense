using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLand : Enemy, IOnLand
{
    [SerializeField] private LayerMask tileLayer;

    private (int, int)[] path;
    private Transform target;
    private int currentPath_index = 0;

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

    public override void Moving()
    {
        if (centerModule == null) return;

        if (Vector2.Distance(tf.position, centerModule.position) <= atk_range) return;

        if (path == null)
        {
            path = findPath();
        }
        else
        {
            target = GridTileManager.Instance.GetWolrdGridTileFromCoordinate(path[currentPath_index].Item2, path[currentPath_index].Item1);   

            if (Vector3.Distance(target.position, transform.position) < 0.1f)
            {
                currentPath_index++;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, move_speed * Time.deltaTime);
            Vector2 direct = target.position - transform.position;
            if (direct.y != 0) 
            {
                float angleZ = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, -angleZ);
            }
            
        }
    }

    private (int, int)[] findPath()
    {
        Vector2Int coord = getCoord(tf);
        Vector2Int targetCoord = getCoord(centerModule);
        (int, int)[] path =  AStar.AStarPathfinding.GeneratePathSync(coord.y, coord.x, targetCoord.y, targetCoord.x, GridTileManager.Instance.GetWalkableMap());
        return path;
    }

    private Vector2Int getCoord(Transform root)
    {
        Collider2D col = Physics2D.OverlapCircle(root.position, 0.1f, tileLayer);
        GridTile tile = col.GetComponent<GridTile>();
        return tile.Coordinate;
    }
}
