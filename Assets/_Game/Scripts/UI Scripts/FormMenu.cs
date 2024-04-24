using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormMenu : MonoBehaviour
{
    public void PlayButton()
    {
        GameManager.Instance.ChangeScene(SceneId.SelectLevel);
    }
}
