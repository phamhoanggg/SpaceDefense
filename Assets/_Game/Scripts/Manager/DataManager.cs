using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMB<DataManager>
{
    public GameData gameData;
    [HideInInspector] public bool isLoaded;
    private const string GAME_DATA = "GameData";

    private void OnApplicationPause(bool pause)
    {
        if (pause && isLoaded)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        if (isLoaded)
        {
            SaveData();
        }
    }
    public void LoadData()
    {
        try
        {
            isLoaded = true;

            if (PlayerPrefs.HasKey(GAME_DATA))
            {
                gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(GAME_DATA));
            }

            else gameData = new GameData();
        }
        catch (Exception ex)
        {
            Debug.LogError("Load Data Error:" + ex);
        }
    }

    public void SaveData()
    {
        try
        {
            if (!isLoaded) return;

            if (gameData == null)
            {
                gameData = new GameData();
                Debug.LogError("dataSaved null, backup fail. Reset data");

            }

            PlayerPrefs.SetString(GAME_DATA, JsonUtility.ToJson(gameData));
            PlayerPrefs.Save();
        }
        catch (Exception ex)
        {
            Debug.LogError("Save Data Error:" + ex);
        }
    }

    public void ChangeResourceAmount(int resIndex, int value)
    {
        gameData.resourcesAmounts[resIndex] += value;
        if (gameData.resourcesAmounts[resIndex] < 0) gameData.resourcesAmounts[resIndex] = 0;
    }
}

[System.Serializable]
public class GameData
{
    public int levelUnlocked;
    public int currentLevelIndex;
    public int[] resourcesAmounts;

    public GameData()
    {
        levelUnlocked = 0;
        resourcesAmounts = new int[] { 2000, 2000, 2000, 2000, 2000};
    }
}
