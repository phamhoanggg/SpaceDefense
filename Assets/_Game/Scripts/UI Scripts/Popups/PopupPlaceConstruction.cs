using I2.Loc;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPlaceConstruction : PopupBase
{
    [SerializeField] private TMP_Text ConstructionName_tmp;
    [SerializeField] private Text ConstructionDes_tmp;
    [SerializeField] private TMP_Text ConstructionMat_tmp;
    [SerializeField] private RectTransform directArrow;
    public override void Open(object args = null)
    {
        base.Open();
        InputManager.Instance.ChangeState(InputManager.Instance.PlaceConstructionState);
        FormGameplay.Instance.Selecting_block.SetActive(true);
        ConstructionName_tmp.text = CoreManager.Instance.selectingPrefab.ConstructionName;
        string term = CoreManager.Instance.selectingPrefab.Description;

        ConstructionDes_tmp.text = LocalizationManager.GetTermTranslation(term);

        directArrow.rotation = Quaternion.Euler(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
    }
    public override void Close()
    {
        base.Close();
        CoreManager.Instance.selectingPrefab = null;
        CoreManager.Instance.selectingConstruction = null;
        InputManager.Instance.ChangeState(InputManager.Instance.DefaultState);
        FormGameplay.Instance.Selecting_block.SetActive(false);
        InputManager.Instance.Selecting_Block.SetActive(false);
    }

    private void Update()
    {
        if (CoreManager.Instance.selectingPrefab)
        {
            string mat_text = "";
            List<ResourceData> matList = CoreManager.Instance.selectingPrefab.buildResources;
            for (int i = 0; i < matList.Count; i++)
            {
                mat_text += $"<sprite={(int)matList[i].res_type}> {DataManager.Instance.gameData.resourcesAmounts[(int)matList[i].res_type]} / {matList[i].res_amount} \n";
            }
            ConstructionMat_tmp.text = mat_text;
        }
        
    }

    #region UI Events
    public void ButtonConfirm()
    {
        for (int i = 0; i < CoreManager.Instance.placingConstructionList.Count; i++)
        {
            CoreManager.Instance.placingConstructionList[i].PlayAnimPrepare(false);
            CoreManager.Instance.placingConstructionList[i].Place();
        }
        if (DataManager.Instance.gameData.currentLevelIndex == -1 && (TutorialController.Instance.CurrentTut_index == 8 || TutorialController.Instance.CurrentTut_index == 9))
        {
            TutorialController.Instance.NextTutorial(0.5f);
        }

        CoreManager.Instance.placingConstructionList.Clear();
        Close();
    }

    public void ButtonRotate()
    {
        CoreManager.Instance.ConstructionDirect -= 1;
        if (CoreManager.Instance.ConstructionDirect == -4)
        {
            CoreManager.Instance.ConstructionDirect = 0;
        }
        if (CoreManager.Instance.selectingConstruction)
        {
            CoreManager.Instance.selectingConstruction.TF.eulerAngles = new Vector3(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
        }
        directArrow.rotation = Quaternion.Euler(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
    }

    public void ButtonCancel()
    {
        for (int i = 0; i < CoreManager.Instance.placingConstructionList.Count; i++)
        {
            Destroy(CoreManager.Instance.placingConstructionList[i].gameObject);
        }
        for (int i = 0; i < CoreManager.Instance.placingConstructionList.Count; i++)
        {
            CoreManager.Instance.placingConstructionList[i].RefillResources();
        }
        CoreManager.Instance.placingConstructionList.Clear();
        Close();
    }
    #endregion
}
