using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSkipTutorial : PopupBase
{
    public void YesButton()
    {
        DataManager.Instance.gameData.ReFillResource();
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }

    public void NoButton()
    {
        Close();
    }
}
