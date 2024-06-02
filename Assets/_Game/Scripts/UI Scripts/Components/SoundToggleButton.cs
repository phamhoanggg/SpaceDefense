using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleButton : ToggleButton
{
    private void Start()
    {
        OnInit(DataManager.Instance.gameData.isSFXEnabled);
    }

    public override void OnToggle()
    {
        base.OnToggle();
        DataManager.Instance.gameData.isSFXEnabled = isCurrentOn;
        AudioManager.Instance.ToggleSound();
    }
}
