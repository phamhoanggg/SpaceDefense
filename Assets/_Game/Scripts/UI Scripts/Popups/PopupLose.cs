using TMPro;
using UnityEngine;

public class PopupLose : PopupBase
{
    [SerializeField] private TMP_Text res_remains_tmp;

    private GameData gameData;
    public override void Open(object args = null)
    {
        base.Open();
        gameData = DataManager.Instance.gameData;
        res_remains_tmp.text = "";
        for (int i = 0; i < gameData.resourcesAmounts.Length; i++)
        {
            DataManager.Instance.ChangeResourceAmount(i, -50);
            res_remains_tmp.text += $"<sprite={i}> : {gameData.resourcesAmounts[i]}  (- 50)\n";
        }
        DataManager.Instance.SaveData();
        AudioManager.Instance.PlaySound(SoundId.Lose);
        Invoke(nameof(PauseTime), 0.5f);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
