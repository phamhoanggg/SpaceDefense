using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : GameUnit
{
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float width;
    public PropsType Type;
    public Rigidbody2D Rb2D => rb2D;
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direct = target.position - TF.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(TF.position + direct.normalized * width / 2, direct, 0.2f - width / 2, InputManager.Instance.resourcesLayer);
            Debug.DrawRay(TF.position + direct.normalized * width / 2, direct.normalized * (0.2f - width / 2), Color.red);
            if (hits.Length <= 1)
            {
                TF.position = Vector2.MoveTowards(TF.position, target.position, speed * Time.deltaTime);
            }

            if (Vector2.Distance(TF.position, target.position) == 0)
            {
                target = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_START_POSITION))
        {
            target = collision.transform.parent.GetComponent<Conveyor>().GetOutputPositionOfInput(collision.transform);
        }

        if (collision.CompareTag(GameConstant.TAG_CENTER_MODULE))
        {
            DataManager.Instance.ChangeResourceAmount((int)Type, 1);
            SimplePool.Despawn(this);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (target == null)
        {
            if (collision.CompareTag(GameConstant.TAG_START_POSITION))
            {
                target = collision.transform.parent.GetComponent<Conveyor>().GetOutputPositionOfInput(collision.transform);
            }
        }
    }


}
