using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : PopupBase
{
    [SerializeField] private TMP_Text res_remains_tmp;
    [SerializeField] private Text playTime_tmp;
    [SerializeField] private Text enemy_cleared;

    private GameData gameData;
    public void Open(string playTime, int enemy_count)
    {
        base.Open();
        gameData = DataManager.Instance.gameData;
        res_remains_tmp.text = "";
        for (int i = 0; i < gameData.resourcesAmounts.Length; i++)
        {
            res_remains_tmp.text += $"<sprite={i}> : {gameData.resourcesAmounts[i]} \n";
        }

        playTime_tmp.text = playTime.ToString();
        enemy_cleared.text = enemy_count.ToString();
        AudioManager.Instance.PlaySound(SoundId.Win);
        DataManager.Instance.SaveData();

    }
    public void MainMenuButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
