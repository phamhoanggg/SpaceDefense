using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    [SerializeField] protected float move_speed;
    protected Transform tf;
    protected Transform centerModule;
    [SerializeField] protected float atk_range;
    [SerializeField] protected GameObject indicator_prefab;
    [SerializeField] protected LayerMask raycastLayer;

    protected float cur_HP;
    private bool isDead;
    private GameObject indicator_object;
    public bool IsDead => isDead;
    public Transform TF => tf;
    public float Attack_Range => atk_range;
    // Start is called before the first frame update
    protected void Start()
    {
        tf = transform;
        centerModule = FindObjectOfType<CenterModule>().TF;

        isDead = false;
        indicator_object = Instantiate(indicator_prefab);
        indicator_object.SetActive(false);
        cur_HP = maxHP;
        Debug.Log(indicator_object);
    }

    protected virtual void Update()
    {
        Moving();

        Vector2 rayDirect = tf.position - Player.Instance.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(Player.Instance.transform.position, rayDirect, rayDirect.magnitude, raycastLayer);

        if (hit.collider != null && hit.collider.CompareTag(GameConstant.TAG_CAMERA_BOX))
        {
            indicator_object.SetActive(true);
            indicator_object.transform.position = hit.point;
            float angleZ = Mathf.Atan2(rayDirect.y, rayDirect.x) * Mathf.Rad2Deg;
            indicator_object.transform.eulerAngles = new Vector3(0, 0, angleZ);
        }
        else
        {
            indicator_object.SetActive(false);
        }
    }

    public virtual void Moving(){}

    public void OnDead()
    {
        if (!isDead)
        {
            isDead = true;
            Destroy(indicator_object);
            GamePlayController.Instance.StopAllCoroutines();
            GamePlayController.Instance.StartCoroutine(GamePlayController.Instance.CheckWinLevel());
            ParticlePoolController.Instance.Play(ParticleType.DeathUnit, tf.position);
            Destroy(gameObject);
        }
        
    }


}
