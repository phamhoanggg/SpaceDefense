using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormMenu : MonoBehaviour
{
    public void PlayButton()
    {
        UIManager.Instance.SetActiveBlock(true);
        SceneLoader.Instance.LoadScene(SceneId.SelectLevel);
    }
}
