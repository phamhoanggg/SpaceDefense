using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMB<UIManager>
{
    [SerializeField] private Transform canvasPopupTransform;
    [SerializeField] private GameObject blocker_obj;

    public bool HaveActivePopup()
    {
        foreach (Transform child in canvasPopupTransform)
        {
            if (child.gameObject.activeInHierarchy) return true;
        }

        return false;
    }

    public void SetActiveBlock(bool isActive)
    {
        blocker_obj.SetActive(isActive);
    }
}
