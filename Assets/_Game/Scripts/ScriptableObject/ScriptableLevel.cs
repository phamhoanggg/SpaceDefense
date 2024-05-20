using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableLevel : ScriptableObject
{
    public int LevelIndex;
    public int Map_Width;
    public int Map_Height;
    public List<MapTile> BG_Tilemap;
    public List<GameObjectTile> Obstacle_Tilemap;
    public List<GameObjectTile> Resource_Tilemap;
    public List<GameObjectTile> Construction_Tilemap;
    public List<EnemyWave> EnemyWaves;
    public List<ResourcesType> resAvailableList;
    public Vector2 centerModulePostion;
}

[System.Serializable]
public class EnemyWave
{
    public EnemyType type;
    public int amount;
    public int spawnTime;
    public Vector2 spawnPosition;
}
