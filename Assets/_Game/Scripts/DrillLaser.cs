using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillLaser : MonoBehaviour
{
    [SerializeField] private GameObject drill_end_point_prefab;
    private Transform startPoint;
    private Transform endPoint;

    private SpriteRenderer spriteRenderer;
    private GameObject drill_point_go;
    public void OnInit(Transform start, Transform end)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        startPoint = start;
        endPoint = end;
        drill_point_go = Instantiate(drill_end_point_prefab, endPoint.position, Quaternion.identity);
        UpdateLaser();
    }

    void Update()
    {
        UpdateLaser();
    }

    void UpdateLaser()
    {
        if (startPoint != null && endPoint != null)
        {
            Vector3 laserDirection = endPoint.position - startPoint.position;
            transform.position = startPoint.position + 0.5f * laserDirection;
            transform.right = laserDirection.normalized;
            float distance = Vector3.Distance(startPoint.position, endPoint.position);
            spriteRenderer.size = new Vector2(distance, spriteRenderer.size.y);

            Vector2 direct = endPoint.position - startPoint.position;
            float angleZ = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            drill_point_go.transform.eulerAngles = new Vector3(0, 0, angleZ);

            if (Vector2.Distance(startPoint.position, endPoint.position) > Player.Instance.Drill_Distance)
            {
                Player.Instance.ChangeState(Player.Instance.normalState);
                Destroy(drill_point_go);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(drill_point_go);
            Destroy(gameObject);
        }
    }
}
