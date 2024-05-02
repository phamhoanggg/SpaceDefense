using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.SelectLevel);
    }
}
