using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConstructionTree : PopupBase
{
    [SerializeField] private SimpleScrollSnap scrolSnap;
    [SerializeField] private Transform contentParent;
    [SerializeField] private ItemConstruction itemPrefab;
    [SerializeField] private TMP_Text constructionIndex_txt;

    private Construction[] constructionSet;

    [SerializeField] private GameObject unlockInfor_go;
    [SerializeField] private GameObject buildInfor_go;

    [Header("CONSTRUCTION PROPERTIES")]
    [SerializeField] private Image constructionIcon;
    [SerializeField] private Text constructionName;
    [SerializeField] private TMP_Text unlockResourcesText;
    [SerializeField] private TMP_Text buildResourcesText;
    [SerializeField] private Button unlockButton;

    private Construction currentConstructionCentered;

    protected override void Awake()
    {
        base.Awake();
        SetupConstructionInfor();
    }
    public override void Open()
    {
        base.Open();
        InputManager.Instance.SetBlockInput(true);
    }
    public override void Close()
    {
        base.Close();
        InputManager.Instance.SetBlockInput(false);

        scrolSnap.GoToPanel(0);
        SetSelectedPanel();
    }

    public void SetupConstructionInfor()
    {
        constructionSet = GameManager.Instance.gameConfig.ConstructionDataSO.constructionSet;
        for (int i = 0; i < constructionSet.Length; i++)
        {
            ItemConstruction newItem = Instantiate(itemPrefab, contentParent);
            newItem.OnInit(constructionSet[i].Info);
        }
    }

    public void SetResourceRequire(ConstructionData constructionData)
    {
        if (constructionData.IsUnlocked)
        {
            buildInfor_go.SetActive(true);
            unlockInfor_go.SetActive(false);
            buildResourcesText.text = "";
            for (int i = 0; i < constructionData.buildResources.Count; i++)
            {
                buildResourcesText.text += $"<sprite={(int)constructionData.buildResources[i].res_type}> {DataManager.Instance.gameData.resourcesAmounts[(int)constructionData.buildResources[i].res_type]}/{constructionData.buildResources[i].res_amount}\n";
            }
        }
        else
        {
            buildInfor_go.SetActive(false);
            unlockInfor_go.SetActive(true);
            unlockResourcesText.text = "";
            for (int i = 0; i < constructionData.unlockResources.Count; i++)
            {
                unlockResourcesText.text += $"<sprite={(int)constructionData.unlockResources[i].res_type}>: {constructionData.unlockResources[i].res_amount}/{DataManager.Instance.gameData.resourcesAmounts[(int)constructionData.unlockResources[i].res_type]}\n";
            }
        }
    }
    

    public void SetSelectedPanel()
    {
        int panelCentered = scrolSnap.CenteredPanel;
        Debug.Log("Centered Panel: " + (panelCentered));
        ConstructionData constructionData = constructionSet[panelCentered].Info;
        constructionIcon.sprite = constructionData.avatarSprite;
        constructionName.text = constructionData.ConstructionName;
        unlockButton.gameObject.SetActive(!constructionData.IsUnlocked);

        SetResourceRequire(constructionData);

        constructionIndex_txt.text = $"{panelCentered + 1}/{constructionSet.Length}";

        currentConstructionCentered = constructionSet[panelCentered];
    }

    public void UnlockConstruction()
    {
        bool isEnough = true;

        for (int i = 0; i < currentConstructionCentered.Info.unlockResources.Count; i++)
        {
            ResourceData resData = currentConstructionCentered.Info.unlockResources[i];
            if (DataManager.Instance.gameData.resourcesAmounts[(int)resData.res_type] < resData.res_amount)
            {
                isEnough = false;
                break;
            }
        }

        if (isEnough)
        {
            for (int i = 0; i < currentConstructionCentered.Info.unlockResources.Count; i++)
            {
                ResourceData resData = currentConstructionCentered.Info.unlockResources[i];
                DataManager.Instance.gameData.resourcesAmounts[(int)resData.res_type] -= resData.res_amount;
            }

            currentConstructionCentered.Info.IsUnlocked = true;
            DataManager.Instance.SaveData();
            SetSelectedPanel();
        }
    }
}
