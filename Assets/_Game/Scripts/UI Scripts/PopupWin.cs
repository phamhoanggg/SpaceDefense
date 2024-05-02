using TMPro;
using UnityEngine;

public class PopupWin : PopupBase
{
    [SerializeField] private TMP_Text res_remains_tmp;

    private GameData gameData;
    public override void Open()
    {
        base.Open();
        gameData = DataManager.Instance.gameData;
        if (gameData.currentLevelIndex == gameData.levelUnlocked)
        {
            gameData.levelUnlocked++;
            DataManager.Instance.SaveData();
        }
        res_remains_tmp.text = "";
        for (int i = 0; i < gameData.resourcesAmounts.Length; i++)
        {
            res_remains_tmp.text += $"<sprite={i}> : {gameData.resourcesAmounts[i]} \n";
        }
        DataManager.Instance.SaveData();

    }
    public void MainMenuButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
