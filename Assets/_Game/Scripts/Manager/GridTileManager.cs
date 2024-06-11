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
        tile_array = new GridTile[height, width];
        walkableMap = new bool[height, width];

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GridTile newTile = Instantiate(prefabTile, new Vector3(x/2f, y/2f, 0), Quaternion.identity, tileParent);
                newTile.name = "TILE (" + x + "," + y + ")";
                tile_array[height - 1 - y, x] = newTile;
                newTile.SetCoord(height - 1 - y, x);
                walkableMap[height - 1 - y, x] = newTile.IsWalkable();
            }
        }

        GetMapSize();
    }

    public void GetMapSize()
    {
        Debug.Log(walkableMap.GetLength(0) + "---" + walkableMap.GetLength(1));
    }

    public bool[,] GetWalkableMap()
    {
        return walkableMap;
    }

    public Transform GetWolrdGridTileFromCoordinate(int x, int y)
    {
        return tile_array[x, y].transform;
    }
}
