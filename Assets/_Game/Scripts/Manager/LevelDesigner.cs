using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum LayerEditing
{
    BackgroundLayer,
    ObstacleLayer,
    ResourceLayer,
    ConstructionLayer,
}

#if UNITY_EDITOR
[ExecuteInEditMode]
public class LevelDesigner : MonoBehaviour
{
    private ScriptableLevel edittingLevel;
    [Header("LAYER LIST")]
    [SerializeField] private Tilemap BGLayer;
    [SerializeField] private GameObject ObsLayer;
    [SerializeField] private GameObject ResLayer;
    [SerializeField] private GameObject ConsLayer;

    [Header("LEVEL NAVIGATE")]
    [InlineButton("PreviousLevel")]
    [InlineButton("NextLevel")]
    [InlineButton("LoadLevel")]
    [SerializeField] int levelIndex;

    [Header("LEVEL INFO")]
    [SerializeField] private List<EnemyWave> enemyWaves;
    [SerializeField] private Vector2 centerModulePosition;

    #region BUTTONS
    [Button(ButtonSizes.Medium), HorizontalGroup("Row1")]
    public void ClearHierarchy()
    {
        TilemapManager.Instance.ClearMap();
        enemyWaves.Clear();
    }

    [Button(ButtonSizes.Medium), HorizontalGroup("Row1")]
    public void SaveLevel()
    {
        TilemapManager.Instance.SaveMap(levelIndex, enemyWaves, centerModulePosition);
        edittingLevel = null;
    }

    #endregion

    #region INLINE BUTTONS
    public void LoadLevel()
    {
        edittingLevel = TilemapManager.Instance.LoadMap(levelIndex);
        enemyWaves = edittingLevel.EnemyWaves;
        centerModulePosition = edittingLevel.centerModulePostion;
    }

    public void NextLevel()
    {

    }

    public void PreviousLevel()
    {

    }
    #endregion
}

#endif
