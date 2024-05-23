using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileManager : SingletonMB<GridTileManager>
{
    [SerializeField] private GridTile prefabTile;
    [SerializeField] Transform tileParent;
    private GridTile[,] tile_array;
    private bool[,] walkableMap;

    public void SpawnAllTiles(int width, int height)
    {
        tile_array = new GridTile[width, height];
        walkableMap = new bool[width, height];

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GridTile newTile = Instantiate(prefabTile, new Vector3(x/2f, y/2f, 0), Quaternion.identity, tileParent);
                newTile.name = "TILE (" + x + "," + y + ")";
                tile_array[x, y] = newTile;
                newTile.SetCoord(x, y);
                walkableMap[x, y] = newTile.IsWalkable();
            }
        }
    }

    public bool[,] GetWalkableMap()
    {
        return walkableMap;
    }
}
