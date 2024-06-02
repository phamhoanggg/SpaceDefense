using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Construction, IDriller
{
    [SerializeField] private float drillSpeed;
    [SerializeField] private Animator fanAnim;
    public List<Transform> outputTransformList;

    private float drill_CD;
    private PoolType drillingType;
    private bool isPlacable;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        if (!isPlacable)
        {
            RefillResources();
            InputManager.Instance.Selecting_Block.SetActive(false);
            CoreManager.Instance.placingConstructionList.Remove(this);
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (isPlaced)
        {
            if (outputTransformList.Count > 0)
            {
                Drilling();
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_ORE))
        {
            isPlacable = true;
            drillingType = collision.GetComponent<Mine>().propPoolType;
        }
    }

    public override void Place()
    {
        base.Place();
        if (!isPlacable)
        {
            RefillResources();
            DestroyBeforePlaced();
        }
        else
        {
            PlayFanAnim(false);
        }
    }

    public void Drilling()
    {
        if (drill_CD > 0 && isFanRotating)
        {
            drill_CD -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < outputTransformList.Count; i++)
            {
                Debug.DrawRay(outputTransformList[i].position - Vector3.forward * 5, Vector3.forward * 100, Color.red, 10);
                if(!Physics2D.Raycast(outputTransformList[i].position - Vector3.forward * 5, Vector3.forward, 100, InputManager.Instance.resourcesLayer))
                {
                    if (!isFanRotating)
                    {
                        PlayFanAnim(true);
                    }

                    Props newProp = SimplePool.Spawn<Props>(drillingType);
                    newProp.TF.position = outputTransformList[i].position;
                    drill_CD = 1 / drillSpeed;
                    
                    break;
                }
                else
                {
                    PlayFanAnim(false);
                    drill_CD = 1 / drillSpeed;
                }
            }
            
        }
    }

    bool isFanRotating = false;
    public void PlayFanAnim(bool isRotating)
    {
        if (!isPlaced) return;

        if (isRotating)
        {
            fanAnim.ResetTrigger(GameConstant.ANIM_DRILL_FAN_IDLE);
            fanAnim.SetTrigger(GameConstant.ANIM_DRILL_FAN_ROTATE);
        }
        else
        {
            fanAnim.ResetTrigger(GameConstant.ANIM_DRILL_FAN_ROTATE);
            fanAnim.SetTrigger(GameConstant.ANIM_DRILL_FAN_IDLE);
        }
        isFanRotating = isRotating;
    }
}
