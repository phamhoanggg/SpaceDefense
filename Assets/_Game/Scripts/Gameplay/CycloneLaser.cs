using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneLaser : MonoBehaviour
{
    [SerializeField] private LaserPoint drill_end_point_prefab;
    private Transform startPoint;
    private Transform endPoint;

    private SpriteRenderer spriteRenderer;
    private LaserPoint drill_point_go;

    private float maxDistance;
    public void OnInit(Transform start, Transform end, float maxDis, float dps)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        startPoint = start;
        endPoint = end;
        drill_point_go = Instantiate(drill_end_point_prefab, endPoint.position, Quaternion.identity);
        drill_point_go.OnInit(dps);
        maxDistance = maxDis;
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
            drill_point_go.transform.position = endPoint.position;


            if (Vector2.Distance(startPoint.position, endPoint.position) > maxDistance)
            {
                Destroy(drill_point_go.gameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(drill_point_go.gameObject);
            gameObject.SetActive(false);
        }
    }
}
