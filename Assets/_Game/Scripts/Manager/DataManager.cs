using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMB<DataManager>
{
    public GameData gameData;

    private void Start()
    {
        gameData = new GameData();
    }
    public void LoadData()
    {

    }

    public void SaveData()
    {
        
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
        resourcesAmounts = new int[] { 200, 200, 200, 200, 200};
    }
}
