using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggleButton : ToggleButton
{
    private void Start()
    {
        OnInit(DataManager.Instance.gameData.isMusicEnabled);
    }

    public override void OnToggle()
    {
        base.OnToggle();
        DataManager.Instance.gameData.isMusicEnabled = isCurrentOn;
        AudioManager.Instance.ToggleMusic();
    }
}
