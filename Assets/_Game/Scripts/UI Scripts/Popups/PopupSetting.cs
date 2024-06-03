using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : PopupBase
{
    public override void Open(object args = null)
    {
        base.Open(args);
        UIManager.Instance.SetActiveBlock(true);
        if (SceneLoader.Instance.CurrentSceneId == SceneId.GamePlay)
        {
            Invoke(nameof(PauseTime), 0.5f);
            InputManager.Instance.SetBlockInput(true);
        }
    }
    public void CloseButton()
    {
        base.Close();
        UIManager.Instance.SetActiveBlock(false);
    }

    public void ButtonContinue()
    {
        Time.timeScale = 1;
        UIManager.Instance.SetActiveBlock(false);
        InputManager.Instance.SetBlockInput(false);
        Close();
    }

    public void ButtonExit()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(SceneId.Menu);
        Close();
    }
}
