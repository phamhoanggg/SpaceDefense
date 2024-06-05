using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormMenu : MonoBehaviour
{
    [SerializeField] private PopupSetting popupSetting;
    private void Start()
    {
        AudioManager.Instance.PlayMusic(MusicId.Menu);
    }
    public void PlayButton()
    {
        UIManager.Instance.SetActiveBlock(true);
        SceneLoader.Instance.LoadScene(SceneId.SelectLevel);
        AudioManager.Instance.PlaySound(SoundId.Click);
    }

    public void TutorialButton()
    {
        UIManager.Instance.SetActiveBlock(true);
        DataManager.Instance.gameData.currentLevelIndex = -1;

        SceneLoader.Instance.LoadScene(SceneId.GamePlay);
        AudioManager.Instance.PlaySound(SoundId.Click);
    }

    public void SettingButton()
    {
        popupSetting.Open();
        AudioManager.Instance.PlaySound(SoundId.Click);
    }
}
