using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Tilemap))]

public class tilesmanager : MonoBehaviour
{

    Tilemap tilemap;

    gridManager grid;

    
    [SerializeField] TileSet tileSet;
    
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<gridManager>();
        //grid.Init(100, 100);
        //Set(1, 1, 2);
        //Set(1, 2, 2);
        //Set(2, 1, 2);
        UpdateTileMap();
    }

    void UpdateTileMap()
    {
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                UpdateTile(x, y);
            }
        }
    }

    private void UpdateTile(int x, int y)
    {
        int tileId = grid.Get(x, y);
        if(tileId == -1)
        {
            return;
        }
        tilemap.SetTile(new Vector3Int(x,y,0), tileSet.tiles[tileId]);
    }

    public void Set(int x, int y, int to)
    {
        grid.Set(x, y, to);
        UpdateTile(x, y);
    }

}
