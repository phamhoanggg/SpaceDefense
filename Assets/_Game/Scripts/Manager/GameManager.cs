using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMB<GameManager>
{
    [Header("GAME CONFIG")]
    public GameConfig gameConfig;

}

[System.Serializable]
public class GameConfig
{
    public ConstructionDataSO ConstructionDataSO;

    public bool isOneHit;
    public bool isUndying;
}
