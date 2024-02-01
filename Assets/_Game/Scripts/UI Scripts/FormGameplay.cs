using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormGameplay : SingletonMB<FormGameplay>
{
    public GameObject Selecting_block;
    [SerializeField] private GameObject[] listPanel;
    [SerializeField] private GameObject[] panelButtons;
    [SerializeField] private GameObject selectingPanel_obj;
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
        CoreManager.Instance.selectingContruction = null;
        CoreManager.Instance.selectingPrefab = null;
    }
    public void OpenDrillPanel()
    {
        OpenPanel(0);
    }

    public void OpenConveyorPanel()
    {
        OpenPanel(1);
    }

    public void OpenTurretPanel()
    {
        OpenPanel(2);
    }

    public void OpenDefenderPanel()
    {
        OpenPanel(3);
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenPopupPause();
    }
}
