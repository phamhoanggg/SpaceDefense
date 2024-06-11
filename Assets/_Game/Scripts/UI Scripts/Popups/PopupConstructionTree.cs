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

    private ConstructionData[] constructionSet;

    [SerializeField] private GameObject unlockInfor_go;
    [SerializeField] private GameObject buildInfor_go;
    [SerializeField] private GameObject require_txt_obj;

    [Header("CONSTRUCTION PROPERTIES")]
    [SerializeField] private Image constructionIcon;
    [SerializeField] private Text constructionName;
    [SerializeField] private TMP_Text unlockResourcesText;
    [SerializeField] private TMP_Text buildResourcesText;
    [SerializeField] private Text requireConstructionText;
    [SerializeField] private Button unlockButton;

    private Construction currentConstructionCentered;
    private static readonly List<ItemConstruction> itemList = new List<ItemConstruction>();
    private GameData gameData => DataManager.Instance.gameData;
    protected override void Awake()
    {
        base.Awake();
        SetupConstructionInfor();
    }
    public override void Open(object args = null)
    {
        base.Open();
        InputManager.Instance.SetBlockInput(true);

        scrolSnap.GoToPanel(0);
        SetSelectedPanel();
    }
    public override void Close()
    {
        InputManager.Instance.SetBlockInput(false);

        base.Close();
    }

    public void SetupConstructionInfor()
    {
        constructionSet = GameManager.Instance.gameConfig.ConstructionDataSO.constructionSet;
        for (int i = 0; i < constructionSet.Length; i++)
        {
            ItemConstruction newItem = Instantiate(itemPrefab, contentParent);
            newItem.OnInit(constructionSet[i]);
            itemList.Add(newItem);
        }
    }

    public void SetResourceRequire(ConstructionData constructionData)
    {
        if (constructionData.IsUnlocked)
        {
            buildInfor_go.SetActive(true);
            unlockInfor_go.SetActive(false);
            buildResourcesText.text = "";
            for (int i = 0; i < constructionData.constructionPrefab.buildResources.Count; i++)
            {
                buildResourcesText.text += $"<sprite={(int)constructionData.constructionPrefab.buildResources[i].res_type}> {DataManager.Instance.gameData.resourcesAmounts[(int)constructionData.constructionPrefab.buildResources[i].res_type]}/{constructionData.constructionPrefab.buildResources[i].res_amount}\n";
            }
        }
        else
        {
            buildInfor_go.SetActive(false);
            unlockInfor_go.SetActive(true);
            bool isUnlockable = true;

            unlockResourcesText.text = "";
            List<ResourceData> unlockResources = constructionData.constructionPrefab.unlockResources;
            for (int i = 0; i < constructionData.constructionPrefab.unlockResources.Count; i++)
            {
                unlockResourcesText.text += $"<sprite={(int)unlockResources[i].res_type}>: {unlockResources[i].res_amount}/{gameData.resourcesAmounts[(int)unlockResources[i].res_type]}\n";

                if (gameData.resourcesAmounts[(int)unlockResources[i].res_type] < unlockResources[i].res_amount) isUnlockable = false;
            }

            require_txt_obj.SetActive(constructionData.parent_Required_ID.Length > 0);
            requireConstructionText.text = "";
            for (int i = 0; i < constructionData.parent_Required_ID.Length; i++)
            {
                int parentID = constructionData.parent_Required_ID[i];
                if (constructionSet[parentID].IsUnlocked)
                {
                    requireConstructionText.text += $"<color=green>{constructionSet[parentID].constructionPrefab.ConstructionName}</color>  ";
                }
                else
                {
                    requireConstructionText.text += $"<color=red>{constructionSet[parentID].constructionPrefab.ConstructionName}</color>  ";
                    isUnlockable = false;
                }
            }

            unlockButton.interactable = isUnlockable;
        }
    }
    

    public void SetSelectedPanel()
    {
        int panelCentered = scrolSnap.CenteredPanel;
        Debug.Log("Centered Panel: " + (panelCentered));
        ConstructionData constructionData = constructionSet[panelCentered];
        constructionIcon.sprite = constructionData.constructionPrefab.avatarSprite;
        constructionName.text = constructionData.constructionPrefab.ConstructionName;

        SetResourceRequire(constructionData);

        constructionIndex_txt.text = $"{panelCentered + 1}/{constructionSet.Length}";

        currentConstructionCentered = constructionSet[panelCentered].constructionPrefab;
    }

    public void UnlockConstruction()
    {
        for (int i = 0; i < currentConstructionCentered.unlockResources.Count; i++)
        {
            ResourceData resData = currentConstructionCentered.unlockResources[i];
            DataManager.Instance.gameData.resourcesAmounts[(int)resData.res_type] -= resData.res_amount;
        }

        constructionSet[scrolSnap.CenteredPanel].IsUnlocked = true;
        DataManager.Instance.SaveData();
        SetSelectedPanel();
        int panelCentered = scrolSnap.CenteredPanel;
        itemList[panelCentered].OnInit(constructionSet[panelCentered]);

        GameManager.Instance.gameConfig.ConstructionDataSO.constructionSet = constructionSet;
        FormGameplay.Instance.UnlockConstruction(panelCentered);
    }
}
