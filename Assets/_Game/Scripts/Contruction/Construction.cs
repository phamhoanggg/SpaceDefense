using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour, IOnLand
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform tf;
    public ConstructionData Info;
    public int Construction_Width;
    public float maxHP, curHP;  

    public Transform TF => tf;

    public bool isPlaced;
    private bool isCollided;
    public void PlayAnimPrepare(bool isPreparing)
    {
        if (isPreparing)
        {
            anim.ResetTrigger(GameConstant.ANIM_Construction_PLACED);
            anim.SetTrigger(GameConstant.ANIM_Construction_PREPARE);
        }
        else
        {
            anim.ResetTrigger(GameConstant.ANIM_Construction_PREPARE);
            anim.SetTrigger(GameConstant.ANIM_Construction_PLACED);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_Construction) && !isPlaced)
        {
            isCollided = true;
        }
    }

    public virtual void Place()
    {
        if (isCollided)
        {
            DestroyConstruction();
        }
        else
        {
            isPlaced = true;
            curHP = maxHP;
        }
    }

    public void DestroyConstruction()
    {
        InputManager.Instance.Selecting_Block.SetActive(false);
        CoreManager.Instance.placingConstructionList.Remove(this);
        Destroy(gameObject);
    }

    public virtual void TakeLandDamage(float dmg)
    {
        if (GameManager.Instance.gameConfig.isUndying) return;
        curHP -= dmg;
        if (curHP <= 0)
        {
            ParticlePool.Play(ParticleType.DeathConstruction, tf.position, Quaternion.identity);
            DestroyConstruction();
        }
    }

    #region Resources Related
    public bool IsEnoughResourcesToBuild()
    {

        for (int i = 0; i < Info.buildResources.Count; i++)
        {
            int res_index = (int)Info.buildResources[i].res_type;
            int res_amount = Info.buildResources[i].res_amount;

            if (DataManager.Instance.gameData.resourcesAmounts[res_index] < res_amount) return false;
        }

        return true;
    }

    public void ConsumeResources()
    {
        for (int i = 0; i < Info.buildResources.Count; i++)
        {
            int res_index = (int)Info.buildResources[i].res_type;
            int res_amount = Info.buildResources[i].res_amount;

            DataManager.Instance.gameData.resourcesAmounts[res_index] -= res_amount;
        }
    }

    public void RefillResources()
    {
        for (int i = 0; i < Info.buildResources.Count; i++)
        {
            int res_index = (int)Info.buildResources[i].res_type;
            int res_amount = Info.buildResources[i].res_amount;

            DataManager.Instance.gameData.resourcesAmounts[res_index] += res_amount;
        }
    }

    #endregion
}


