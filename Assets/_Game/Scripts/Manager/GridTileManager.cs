using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileManager : SingletonMB<GridTileManager>
{
    public int width, height;
    [SerializeField] private GridTile prefabTile;
    [SerializeField] Transform tileParent;

    private void Start()
    {
        SpawnAllTiles();
    }
    public void SpawnAllTiles()
    {
        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GridTile newTile = Instantiate(prefabTile, new Vector3(x/2f, y/2f, 0), Quaternion.identity, tileParent);
                newTile.name = "TILE (" + x + "," + y + ")";
            }
        }
    }
}
