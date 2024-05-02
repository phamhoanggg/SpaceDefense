using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMB<UIManager>
{
    [SerializeField] private Transform canvasPopupTransform;

    public bool HaveActivePopup()
    {
        foreach (Transform child in canvasPopupTransform)
        {
            if (child.gameObject.activeInHierarchy) return true;
        }

        return false;
    }
}
