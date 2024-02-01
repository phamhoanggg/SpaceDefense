using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PopupPlaceContruction : PopupBase
{
    [SerializeField] private TMP_Text contructionName_tmp, contructionDes_tmp;
    [SerializeField] private TMP_Text contructionMat_tmp;
    [SerializeField] private RectTransform directArrow;
    public override void Open()
    {
        base.Open();
        InputManager.Instance.ChangeState(InputManager.Instance.PlaceContructionState);
        FormGameplay.Instance.Selecting_block.SetActive(true);
        contructionName_tmp.text = CoreManager.Instance.selectingPrefab.Info.ContructionName;
        contructionDes_tmp.text = CoreManager.Instance.selectingPrefab.Info.Description;

        directArrow.rotation = Quaternion.Euler(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
    }
    public override void Close(UnityAction OnComplete = null)
    {
        base.Close(OnComplete);
        CoreManager.Instance.selectingPrefab = null;
        CoreManager.Instance.selectingContruction = null;
        InputManager.Instance.ChangeState(InputManager.Instance.DefaultState);
        FormGameplay.Instance.Selecting_block.SetActive(false);
        InputManager.Instance.Selecting_Block.SetActive(false);
    }

    private void Update()
    {
        if (CoreManager.Instance.selectingPrefab)
        {
            string mat_text = "";
            List<BuildMaterial> matList = CoreManager.Instance.selectingPrefab.Info.materialList;
            for (int i = 0; i < matList.Count; i++)
            {
                mat_text += $"<sprite={(int)matList[i].res_type}> {DataManager.Instance.gameData.resourcesAmounts[(int)matList[i].res_type]} / {matList[i].res_amount} \n";
            }
            contructionMat_tmp.text = mat_text;
        }
        
    }

    #region UI Events
    public void ButtonConfirm()
    {
        for (int i = 0; i < CoreManager.Instance.placingContructionList.Count; i++)
        {
            CoreManager.Instance.placingContructionList[i].PlayAnimPrepare(false);
            CoreManager.Instance.placingContructionList[i].Place();
        }

        CoreManager.Instance.placingContructionList.Clear();
        Close();
    }

    public void ButtonRotate()
    {
        CoreManager.Instance.ConstructionDirect -= 1;
        if (CoreManager.Instance.ConstructionDirect == -4)
        {
            CoreManager.Instance.ConstructionDirect = 0;
        }
        if (CoreManager.Instance.selectingContruction)
        {
            CoreManager.Instance.selectingContruction.TF.eulerAngles = new Vector3(0, 0, 90 * CoreManager.Instance.ConstructionDirect);
        }
        directArrow.rotation = Quaternion.Euler(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
    }

    public void ButtonCancel()
    {
        for (int i = 0; i < CoreManager.Instance.placingContructionList.Count; i++)
        {
            Destroy(CoreManager.Instance.placingContructionList[i].gameObject);
        }
        for (int i = 0; i < CoreManager.Instance.placingContructionList.Count; i++)
        {
            CoreManager.Instance.placingContructionList[i].RefillResources();
        }
        CoreManager.Instance.placingContructionList.Clear();
        Close();
    }
    #endregion
}
