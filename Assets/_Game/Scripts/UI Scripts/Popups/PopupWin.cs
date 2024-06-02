using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : PopupBase
{
    [SerializeField] private TMP_Text res_remains_tmp;
    [SerializeField] private Text playTime_tmp;

    private GameData gameData;
    public override void Open(object playTime)
    {
        base.Open();
        gameData = DataManager.Instance.gameData;
        if (gameData.currentLevelIndex == gameData.levelUnlocked)
        {
            gameData.levelUnlocked++;
            DataManager.Instance.gameData = gameData;
        }
        res_remains_tmp.text = "";
        for (int i = 0; i < gameData.resourcesAmounts.Length; i++)
        {
            res_remains_tmp.text += $"<sprite={i}> : {gameData.resourcesAmounts[i]} \n";
        }

        playTime_tmp.text = playTime.ToString();

        DataManager.Instance.SaveData();

    }
    public void MainMenuButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
