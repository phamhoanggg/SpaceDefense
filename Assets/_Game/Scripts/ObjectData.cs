using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelDataWrapper
{
    public List<LevelData> levelList;
}

[System.Serializable]
public class LevelData
{
    public List<MapTile> BG_Tilemap;
    public List<MapTile> Obstacle_Tilemap;
    public List<GameObjectTile> Resource_Tilemap;
    public List<GameObjectTile> Construction_Tilemap;
}

[System.Serializable]
public class GameObjectTile
{
    public Vector2 position;
    public GameObjectType go_type;
    public int type_index;
}

[System.Serializable]
public class MapTile
{
    public Vector3Int position;
    public Tile levelTile;
}


public enum GameObjectType
{
    resource,
    construction
}



