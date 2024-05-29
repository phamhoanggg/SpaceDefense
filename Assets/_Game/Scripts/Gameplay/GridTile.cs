using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private Vector2Int coordinate;
    public Vector2Int Coordinate => coordinate;

    public LayerMask constructionLayer, obstacleLayer;

    public void OnInit(int x, int y)
    {
        SetCoord(x, y);

    }

    public void SetCoord(int x, int y)
    {
        coordinate = new Vector2Int(x, y);
    }

    public bool IsWalkable()
    {
        Collider2D col1 = Physics2D.OverlapCircle(transform.position, 0.1f, constructionLayer);
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, 0.1f, obstacleLayer);
        return col1 == null && col2 == null;
    }
}
