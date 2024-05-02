using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupConstructionTree : PopupBase
{
    [SerializeField] private SimpleScrollSnap scrolSnap;
    private ConstructionData[] constructionSet;

    [Header("CONSTRUCTION PROPERTIES")]
    [SerializeField] private Image constructionIcon;
    [SerializeField] private Text constructionName;
    [SerializeField] private TMP_Text resourcesText;
    
    public override void Open()
    {
        base.Open();
        InputManager.Instance.SetBlockInput(true);
        constructionSet = GameManager.Instance.gameConfig.ConstructionDataSO.constructionSet;
        scrolSnap.GoToPanel(0);
        SetSelectedPanel();
    }

    public override void Close()
    {
        base.Close();
        InputManager.Instance.SetBlockInput(false);
    }

    public void SetSelectedPanel()
    {
        int panelIndex = scrolSnap.SelectedPanel;
        Debug.Log("Set Selected Panel: " + (panelIndex));
        ConstructionData constructionData = constructionSet[panelIndex];
        constructionIcon.sprite = constructionData.ConstructionImage;
        constructionName.text = constructionData.ConstructionName;
    }
}
