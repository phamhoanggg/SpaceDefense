using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private TimeFetcher timeFetcher;

    private GameData gameData => DataManager.Instance.gameData;
    private GameConfig gameConfig => GameManager.Instance.gameConfig;

    private IEnumerator Start()
    {
        DOVirtual.Float(0f, 0.6f, 3f, value =>
        {
            SetProgress(value);
        });


        yield return new WaitForSeconds(1f);

        DataManager.Instance.LoadData();
        AudioManager.Instance.Initialize();

        yield return new WaitForSeconds(2f);

        if (gameData.isFirstOpen)
        {
            DataManager.Instance.gameData.isFirstOpen = false;
            SceneLoader.Instance.LoadScene(SceneId.Menu, SceneLoader.Mode.Before, (float progress) =>
            {
                SetProgress(0.6f + progress / 0.9f * 0.4f);
            });
        }
        else
        {
            SceneLoader.Instance.LoadScene(SceneId.Menu, SceneLoader.Mode.Before, (float progress) =>
            {
                SetProgress(0.6f + progress / 0.9f * 0.4f);
            });
        }
        
    }

    private void SetProgress(float progress)
    {
        fillImage.fillAmount = progress;
    }
}
