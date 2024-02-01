using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMB<UIManager>
{
    [SerializeField] private Transform canvasPopupTransform;

    [Header("POPUP PREFABS")]
    [SerializeField] private PopupPlaceContruction prefabPopupPlaceContruction;
    [SerializeField] private PopupWin prefabPopupWin;
    [SerializeField] private PopupPause prefabPopupPause;
    [SerializeField] private PopupLose prefabPopupLose;

    public PopupPlaceContruction PopupPlaceContruction { get; private set; }
    public PopupWin PopupWin { get; private set; }
    public PopupPause PopupPause { get; private set; }
    public PopupLose PopupLose { get; private set; }
    public bool HaveActivePopup()
    {
        foreach (Transform child in canvasPopupTransform)
        {
            if (child.gameObject.activeInHierarchy) return true;
        }

        return false;
    }
    public void OpenPopupPlaceContruction()
    {
        if (PopupPlaceContruction == null)
        {
            PopupPlaceContruction = Instantiate(prefabPopupPlaceContruction, canvasPopupTransform);
        }

        PopupPlaceContruction.Open();
    }

    public void OpenPopupPopupWin()
    {
        if (PopupWin == null)
        {
            PopupWin = Instantiate(prefabPopupWin, canvasPopupTransform);
        }

        PopupWin.Open();
    }

    public void OpenPopupPopupLose()
    {
        if (PopupLose == null)
        {
            PopupLose = Instantiate(prefabPopupLose, canvasPopupTransform);
        }

        PopupLose.Open();
    }

    public void OpenPopupPause()
    {
        if (PopupPause == null)
        {
            PopupPause = Instantiate(prefabPopupPause, canvasPopupTransform);
        }

        PopupPause.Open();
    }
}
