using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : PopupBase
{
    public override void Open(object args = null)
    {
        base.Open(args);
        UIManager.Instance.SetActiveBlock(true);
    }
    public void CloseButton()
    {
        base.Close();
        UIManager.Instance.SetActiveBlock(false);
    }
}
