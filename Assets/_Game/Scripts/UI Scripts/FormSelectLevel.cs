using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormSelectLevel : SingletonMB<FormSelectLevel>
{
    private ScriptableLevel selectingLevel;
    [SerializeField] TMP_Text resListText;
    [SerializeField] TMP_Text enemyListText;

    public GameObject SelectingLevelFrame;
    public void SetLevelSelecting(ScriptableLevel level)
    {
        selectingLevel = level;
        SetResourceListText();
        SetEnemyListText();
    }

    private void SetResourceListText()
    {
        string resListText = "";
        for (int i = 0; i < selectingLevel.resAvailableList.Count; i++)
        {
            resListText += $"<sprite={(int)selectingLevel.resAvailableList[i]}>  ";
        }
        this.resListText.text = resListText;
    }

    private void SetEnemyListText()
    {

    }

    public void PlayButton()
    {
        if (selectingLevel != null)
        {
            UIManager.Instance.SetActiveBlock(true);
            SceneLoader.Instance.LoadScene(SceneId.GamePlay);
            DataManager.Instance.gameData.currentLevelIndex = selectingLevel.LevelIndex;
        }
    }

    public void BackButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
