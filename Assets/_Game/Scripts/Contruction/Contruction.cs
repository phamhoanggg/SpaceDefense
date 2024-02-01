using System.Collections.Generic;
using UnityEngine;

public class Contruction : MonoBehaviour, IOnLand
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform tf;
    public ContructionInfo Info;
    public Sprite avatarSprite;
    public int Contruction_Width;
    public float maxHP, curHP;  

    public Transform TF => tf;

    public bool isPlaced;
    private bool isCollided;
    public void PlayAnimPrepare(bool isPreparing)
    {
        if (isPreparing)
        {
            anim.ResetTrigger(GameConstant.ANIM_CONTRUCTION_PLACED);
            anim.SetTrigger(GameConstant.ANIM_CONTRUCTION_PREPARE);
        }
        else
        {
            anim.ResetTrigger(GameConstant.ANIM_CONTRUCTION_PREPARE);
            anim.SetTrigger(GameConstant.ANIM_CONTRUCTION_PLACED);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.TAG_CONTRUCTION) && !isPlaced)
        {
            isCollided = true;
        }
    }

    public virtual void Place()
    {
        if (isCollided)
        {
            DestroyContruction();
        }
        else
        {
            isPlaced = true;
            curHP = maxHP;
        }
    }

    public void DestroyContruction()
    {
        InputManager.Instance.Selecting_Block.SetActive(false);
        CoreManager.Instance.placingContructionList.Remove(this);
        Destroy(gameObject);
    }

    public virtual void TakeLandDamage(float dmg)
    {
        if (GameManager.Instance.isUndying) return;
        curHP -= dmg;
        if (curHP <= 0)
        {
            ParticlePool.Play(ParticleType.DeathContruction, tf.position, Quaternion.identity);
            DestroyContruction();
        }
    }

    #region Resources Related
    public bool IsEnoughResourcesToBuild()
    {

        for (int i = 0; i < Info.materialList.Count; i++)
        {
            int res_index = (int)Info.materialList[i].res_type;
            int res_amount = Info.materialList[i].res_amount;

            if (DataManager.Instance.gameData.resourcesAmounts[res_index] < res_amount) return false;
        }

        return true;
    }

    public void ConsumeResources()
    {
        for (int i = 0; i < Info.materialList.Count; i++)
        {
            int res_index = (int)Info.materialList[i].res_type;
            int res_amount = Info.materialList[i].res_amount;

            DataManager.Instance.gameData.resourcesAmounts[res_index] -= res_amount;
        }
    }

    public void RefillResources()
    {
        for (int i = 0; i < Info.materialList.Count; i++)
        {
            int res_index = (int)Info.materialList[i].res_type;
            int res_amount = Info.materialList[i].res_amount;

            DataManager.Instance.gameData.resourcesAmounts[res_index] += res_amount;
        }
    }

    #endregion
}

[System.Serializable]
public class BuildMaterial
{
    public ResourcesType res_type;
    public int res_amount;
}

[System.Serializable]
public class ContructionInfo
{
    public string ContructionName;
    public List<BuildMaterial> materialList = new List<BuildMaterial>();
    public string Description;

}
