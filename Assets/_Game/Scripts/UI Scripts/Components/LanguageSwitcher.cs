using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private ToggleButton englishBtn;
    [SerializeField] private ToggleButton vietnamBtn;

    private void Start()
    {
        if (LocalizationManager.CurrentLanguage == "English")
        {
            EnglishButtonClick();
        }
        else
        {
            VietnameseButtonClick();
        }
    }
    public void EnglishButtonClick()
    {
        englishBtn.OnInit(true);
        vietnamBtn.OnInit(false);
    }

    public void VietnameseButtonClick()
    {
        englishBtn.OnInit(false);
        vietnamBtn.OnInit(true);
    }
}
