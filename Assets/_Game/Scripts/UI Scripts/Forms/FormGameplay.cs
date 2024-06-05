using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormGameplay : SingletonMB<FormGameplay>
{
    public GameObject Selecting_block;
    [SerializeField] private GameObject[] listPanel;
    [SerializeField] private GameObject[] panelButtons;
    [SerializeField] private GameObject selectingPanel_obj;
    [SerializeField] private GameObject bottom_object;

    [Header("POPUPS")]
    [SerializeField] private PopupSetting popupPause;
    [SerializeField] private PopupWin popupWin;
    [SerializeField] private PopupLose popupLose;
    [SerializeField] private PopupPlaceConstruction popupPlaceConstruction;
    [SerializeField] private PopupConstructionTree popupConstructionTree;

    private GameObject currentPanel;
    private CenterModule centerModule;

    private void Start()
    {
        selectingPanel_obj.SetActive(false);
        currentPanel = listPanel[0];
        currentPanel.SetActive(true);
        for (int i = 1; i < listPanel.Length; i++)
        {
            listPanel[i].SetActive(false);
        }

        AudioManager.Instance.PlayMusic(MusicId.Game);
        if (DataManager.Instance.gameData.currentLevelIndex == -1) bottom_object.SetActive(false);
    }

    public void DisplayBottomObject()
    {
        bottom_object.SetActive(true);
    }

    void OpenPanel(int index)
    {
        if (currentPanel)
        {
            currentPanel.SetActive(false);
        }
        currentPanel = listPanel[index];
        currentPanel.SetActive(true);
        selectingPanel_obj.SetActive(true);
        selectingPanel_obj.transform.position = panelButtons[index].transform.position;
        CoreManager.Instance.selectingConstruction = null;
        CoreManager.Instance.selectingPrefab = null;
    }

    public void OpenDrillPanel()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1 && TutorialController.Instance.CurrentTut_index == 7)
        {
            TutorialController.Instance.NextTutorial(0);
        }
        OpenPanel(0);
    }

    public void OpenConveyorPanel()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1 && TutorialController.Instance.CurrentTut_index <= 8) return;

        OpenPanel(1);
    }

    public void OpenTurretPanel()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1 && TutorialController.Instance.CurrentTut_index <= 9) return;

        OpenPanel(2);
    }

    public void OpenDefenderPanel()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1) return;

        OpenPanel(3);
    }

    public void SettingButton()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1) return;

        popupPause.Open();
    }

    public void OpenPopupLose()
    {
        popupLose.Open();
    }

    public void OpenPopupWin(string playTime)
    {
        popupWin.Open(playTime);
    }

    public void OpenPopupPlaceConstruction()
    {
        popupPlaceConstruction.Open();
    }

    public void OpenPopupConstructionTree()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1) return;

        popupConstructionTree.Open();
    }
}
