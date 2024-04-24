using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : SingletonMB<TilemapManager>
{
    [SerializeField] private Tilemap BG_Map;
    [SerializeField] private GameObject Res_Map, Constrcution_Map, Obs_Map;

#if UNITY_EDITOR
    public void SaveMap(int levelIdx, List<EnemyWave> enemyWaves, Vector2 centerModulePos)
    {
        var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        newLevel.LevelIndex = levelIdx;
        newLevel.name = $"Level {levelIdx}";
        newLevel.resAvailableList = new List<ResourcesType>();

        newLevel.BG_Tilemap = GetTilesFromMap(BG_Map).ToList();
        newLevel.Obstacle_Tilemap = GetObstacleFromMap(Obs_Map).ToList();
        newLevel.Resource_Tilemap = GetMinesFromMap(Res_Map).ToList();
        newLevel.EnemyWaves = enemyWaves;
        newLevel.centerModulePostion = centerModulePos;

        ScriptableObjectUtility.SaveLevelFile(newLevel);

        IEnumerable<MapTile> GetTilesFromMap(Tilemap map)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    var levelTile = map.GetTile<Tile>(pos);
                    yield return new MapTile()
                    {
                        position = pos,
                        levelTile = levelTile
                    };
                }
            }
        }

        IEnumerable<GameObjectTile> GetMinesFromMap(GameObject map)
        {
            Mine[] mines = map.GetComponentsInChildren<Mine>();
            foreach (var unit in mines)
            {
                if (!newLevel.resAvailableList.Contains(unit.resType)){
                    newLevel.resAvailableList.Add(unit.resType);
                }
                yield return new GameObjectTile()
                {
                    position = unit.transform.localPosition,
                    go_type = MapGameObjectType.resource,
                    type_index = (int)unit.resType
                };
            }
        }

        IEnumerable<GameObjectTile> GetObstacleFromMap(GameObject map)
        {
            Transform[] obstacles = map.GetComponentsInChildren<Transform>();
            foreach (var unit in obstacles)
            {
                yield return new GameObjectTile()
                {
                    position = unit.localPosition,
                    go_type = MapGameObjectType.obstacle,
                    type_index = ResourceController.Instance.obstacleList.IndexOf(unit.gameObject)
                };
            }
        }

        ClearMap();
    }
#endif
    public void ClearMap()
    {
        var maps = FindObjectsOfType<Tilemap>();

        foreach (var tilemap in maps)
        {
            tilemap.ClearAllTiles();
        }

        Mine[] mines = Res_Map.GetComponentsInChildren<Mine>();
        foreach (var unit in mines)
        {
            DestroyImmediate(unit.gameObject);
        }
    }


    public ScriptableLevel LoadMap(int levelIdx)
    {
        ScriptableLevel level = Resources.Load<ScriptableLevel>($"Levels/Level {levelIdx}");
        if (level == null)
        {
            Debug.LogError($"Level {levelIdx} does not exist.");
            return null;
        }

        ClearMap();

        foreach (var tile in level.BG_Tilemap)
        {
            SetTile(BG_Map, tile);
        }

        foreach (var obsTile in level.Obstacle_Tilemap)
        {
            GameObject new_obsTile = Instantiate(ResourceController.Instance.obstacleList[obsTile.type_index], Obs_Map.transform);
            new_obsTile.transform.localPosition = obsTile.position;
        }

        foreach (var resTile in level.Resource_Tilemap)
        {
            GameObject new_resTile = Instantiate(ResourceController.Instance.resourceList[resTile.type_index], Res_Map.transform);
            new_resTile.transform.localPosition = resTile.position;
        }

        void SetTile(Tilemap map, MapTile tile)
        {
            map.SetTile(tile.position, tile.levelTile);
        }

        Debug.Log("Size: " + BG_Map.size);
        CameraController camCtrl = FindObjectOfType<CameraController>();
        if (camCtrl) camCtrl.SetMaxPosition(BG_Map.size.x, BG_Map.size.y);

        return level;
    }


}

#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
    public static void SaveLevelFile(ScriptableLevel level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/_Game/Resources/Levels/{level.name}.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif